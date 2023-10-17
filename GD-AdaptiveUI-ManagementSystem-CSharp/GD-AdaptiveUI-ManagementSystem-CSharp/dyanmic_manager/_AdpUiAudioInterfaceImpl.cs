using System.Collections.Generic;
using System.Runtime.InteropServices;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public partial class _AdpUiAudioInterfaceImpl : Node
{
    private readonly Queue<AudioStreamPlayer> m_AvailableAudioStreamPlayers = new();
    private readonly List<AudioStreamPlayer> m_OccupiedAudioStreamPlayers = new();
    private readonly HashSet<AudioStreamPlayer> m_PreloadedAudioStreamInstances = new();
    private float m_VolumeDb;
    private bool m_VolumeChanged;
    [Export] private uint PreloadAudioStreamPlayerCount { get; set; } = 4;
    [Export] private AudioStream DefaultPanelOpenAudio { get; set; }
    [Export] private AudioStream DefaultPanelCloseAudio { get; set; }

    [Export] public float VolumeDb
    {
        get => m_VolumeDb;
        set
        {
            m_VolumeDb = value;
            m_VolumeChanged = true;
        }
    }

    internal void PlayDefaultPanelOpenAudio() => PlayAudio(DefaultPanelOpenAudio);
    internal void PlayDefaultPanelCloseAudio() => PlayAudio(DefaultPanelCloseAudio);

    internal void Load()
    {
        for (var i = 0; i < PreloadAudioStreamPlayerCount; i++)
        {
            var instance = CreatePlayer($"BufferedPlayer#{i.ToString()}");
            m_AvailableAudioStreamPlayers.Enqueue(instance);
            m_PreloadedAudioStreamInstances.Add(instance);
        }
    }

    private AudioStreamPlayer CreatePlayer(string name)
    {
        var instance = new AudioStreamPlayer();
        instance.Autoplay = false;
        instance.MaxPolyphony = 1;
        instance.Name = name;
        AddChild(instance);
        return instance;
    }

    internal void PlayAudio(AudioStream audioStream)
    {
        if(audioStream == null) return;
        var player = GetOrCreatePlayer();
        player.Stream = audioStream;
        player.Play();
    }
    
    public sealed override void _Process(double delta)
    {
        if (m_OccupiedAudioStreamPlayers.Count == 0) return;

        var span = CollectionsMarshal.AsSpan(m_OccupiedAudioStreamPlayers);

        for (var i = m_OccupiedAudioStreamPlayers.Count - 1; i >= 0; i--)
        {
            var audioStreamPlayer = span[i];

            if(m_VolumeChanged) audioStreamPlayer.VolumeDb = VolumeDb;
            
            if (audioStreamPlayer.Playing) continue;

            m_OccupiedAudioStreamPlayers.RemoveAt(i);

            if (!m_PreloadedAudioStreamInstances.Contains(audioStreamPlayer))
            {
                audioStreamPlayer.QueueFree();
            }
            else
            {
                m_AvailableAudioStreamPlayers.Enqueue(audioStreamPlayer);
                audioStreamPlayer.Stream = null;
            }
        }

        m_VolumeChanged = false;
    }

    private AudioStreamPlayer GetOrCreatePlayer()
    {
        var player =
            m_AvailableAudioStreamPlayers.Count == 0 ?
                CreatePlayer($"TempPlayer#{m_OccupiedAudioStreamPlayers.Count}") :
                m_AvailableAudioStreamPlayers.Dequeue();

        m_OccupiedAudioStreamPlayers.Add(player);

        return player;
    }
}
