using System;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

/// <summary>
/// Store predefined input schemes 
/// </summary>
public static class AdpUIInputScheme
{
    /// <summary>
    /// Default schemes for representing UI interactions
    /// </summary>
    public const uint UIInputScheme = 0;

    /// <summary>
    /// Obtain a predefined list of input action names Godot engine uses for ui interactions in the form of readonly span 
    /// </summary>
    public static ReadOnlySpan<string> BuiltinUIActionStrings => s_BuiltinUIActionStrings;

    /// <summary>
    /// Data class that holds every input action names Godot engine uses for ui interactions
    /// </summary>
    public static class UIActions
    {
        public const string UI_Accept = "ui_accept";
        public const string UI_Select = "ui_select";
        public const string UI_Cancel = "ui_cancel";
        public const string UI_Focus_Next = "ui_focus_next";
        public const string UI_Focus_Prev = "ui_focus_prev";
        public const string UI_Left = "ui_left";
        public const string UI_Right = "ui_right";
        public const string UI_Up = "ui_up";
        public const string UI_Down = "ui_down";
        public const string UI_Page_Up = "ui_page_up";
        public const string UI_Page_Down = "ui_page_down";
        public const string UI_Home = "ui_home";
        public const string UI_End = "ui_end";
        public const string UI_Cut = "ui_cut";
        public const string UI_Copy = "ui_copy";
        public const string UI_Paste = "ui_paste";
        public const string UI_Undo = "ui_undo";
        public const string UI_Redo = "ui_redo";
        public const string UI_Text_Completion_Query = "ui_text_completion_query";
        public const string UI_Text_Completion_Accept = "ui_text_completion_accept";
        public const string UI_Text_Completion_Replace = "ui_text_completion_replace";
        public const string UI_Text_Newline = "ui_text_newline";
        public const string UI_Text_Newline_Blank = "ui_text_newline_blank";
        public const string UI_Text_Newline_Above = "ui_text_newline_above";
        public const string UI_Text_Indent = "ui_text_indent";
        public const string UI_Text_Dedent = "ui_text_dedent";
        public const string UI_Text_Backspace = "ui_text_backspace";
        public const string UI_Text_Backspace_Word = "ui_text_backspace_word";
        public const string UI_Text_Backspace_Word_Macos = "ui_text_backspace_word.macos";
        public const string UI_Text_Backspace_All_To_Left = "ui_text_backspace_all_to_left";
        public const string UI_Text_Backspace_All_To_Left_Macos = "ui_text_backspace_all_to_left.macos";
        public const string UI_Text_Delete = "ui_text_delete";
        public const string UI_Text_Delete_Word = "ui_text_delete_word";
        public const string UI_Text_Delete_Word_Macos = "ui_text_delete_word.macos";
        public const string UI_Text_Delete_All_To_Right = "ui_text_delete_all_to_right";
        public const string UI_Text_Delete_All_To_Right_Macos = "ui_text_delete_all_to_right.macos";
        public const string UI_Text_Caret_Left = "ui_text_caret_left";
        public const string UI_Text_Caret_Word_Left = "ui_text_caret_word_left";
        public const string UI_Text_Caret_Word_Left_Macos = "ui_text_caret_word_left.macos";
        public const string UI_Text_Caret_Right = "ui_text_caret_right";
        public const string UI_Text_Caret_Word_Right = "ui_text_caret_word_right";
        public const string UI_Text_Caret_Word_Right_Macos = "ui_text_caret_word_right.macos";
        public const string UI_Text_Caret_Up = "ui_text_caret_up";
        public const string UI_Text_Caret_Down = "ui_text_caret_down";
        public const string UI_Text_Caret_Line_Start = "ui_text_caret_line_start";
        public const string UI_Text_Caret_Line_Start_Macos = "ui_text_caret_line_start.macos";
        public const string UI_Text_Caret_Line_End = "ui_text_caret_line_end";
        public const string UI_Text_Caret_Line_End_Macos = "ui_text_caret_line_end.macos";
        public const string UI_Text_Caret_Page_Up = "ui_text_caret_page_up";
        public const string UI_Text_Caret_Page_Down = "ui_text_caret_page_down";
        public const string UI_Text_Caret_Document_Start = "ui_text_caret_document_start";
        public const string UI_Text_Caret_Document_Start_Macos = "ui_text_caret_document_start.macos";
        public const string UI_Text_Caret_Document_End = "ui_text_caret_document_end";
        public const string UI_Text_Caret_Document_End_Macos = "ui_text_caret_document_end.macos";
        public const string UI_Text_Caret_Add_Below = "ui_text_caret_add_below";
        public const string UI_Text_Caret_Add_Below_Macos = "ui_text_caret_add_below.macos";
        public const string UI_Text_Caret_Add_Above = "ui_text_caret_add_above";
        public const string UI_Text_Caret_Add_Above_Macos = "ui_text_caret_add_above.macos";
        public const string UI_Text_Scroll_Up = "ui_text_scroll_up";
        public const string UI_Text_Scroll_Up_Macos = "ui_text_scroll_up.macos";
        public const string UI_Text_Scroll_Down = "ui_text_scroll_down";
        public const string UI_Text_Scroll_Down_Macos = "ui_text_scroll_down.macos";
        public const string UI_Text_Select_All = "ui_text_select_all";
        public const string UI_Text_Select_Word_Under_Caret = "ui_text_select_word_under_caret";
        public const string UI_Text_Select_Word_Under_Caret_Macos = "ui_text_select_word_under_caret.macos";
        public const string UI_Text_Add_Selection_For_Next_Occurrence = "ui_text_add_selection_for_next_occurrence";
        public const string UI_Text_Clear_Carets_And_Selection = "ui_text_clear_carets_and_selection";
        public const string UI_Text_Toggle_Insert_Mode = "ui_text_toggle_insert_mode";
        public const string UI_Menu = "ui_menu";
        public const string UI_Text_Submit = "ui_text_submit";
        public const string UI_Graph_Duplicate = "ui_graph_duplicate";
        public const string UI_Graph_Delete = "ui_graph_delete";
        public const string UI_Filedialog_Up_One_Level = "ui_filedialog_up_one_level";
        public const string UI_Filedialog_Refresh = "ui_filedialog_refresh";
        public const string UI_Filedialog_Show_Hidden = "ui_filedialog_show_hidden";
        public const string UI_Swap_Input_Direction = "ui_swap_input_direction";
    }

    /// <summary>
    /// The internal array storing the list of predefined input action names Godot engine uses for ui interactions
    /// </summary>
    private static readonly string[] s_BuiltinUIActionStrings =
        {
            UIActions.UI_Accept,
            UIActions.UI_Select,
            UIActions.UI_Cancel,
            UIActions.UI_Focus_Next,
            UIActions.UI_Focus_Prev,
            UIActions.UI_Left,
            UIActions.UI_Right,
            UIActions.UI_Up,
            UIActions.UI_Down,
            UIActions.UI_Page_Up,
            UIActions.UI_Page_Down,
            UIActions.UI_Home,
            UIActions.UI_End,
            UIActions.UI_Cut,
            UIActions.UI_Copy,
            UIActions.UI_Paste,
            UIActions.UI_Undo,
            UIActions.UI_Redo,
            UIActions.UI_Text_Completion_Query,
            UIActions.UI_Text_Completion_Accept,
            UIActions.UI_Text_Completion_Replace,
            UIActions.UI_Text_Newline,
            UIActions.UI_Text_Newline_Blank,
            UIActions.UI_Text_Newline_Above,
            UIActions.UI_Text_Indent,
            UIActions.UI_Text_Dedent,
            UIActions.UI_Text_Backspace,
            UIActions.UI_Text_Backspace_Word,
            UIActions.UI_Text_Backspace_Word_Macos,
            UIActions.UI_Text_Backspace_All_To_Left,
            UIActions.UI_Text_Backspace_All_To_Left_Macos,
            UIActions.UI_Text_Delete,
            UIActions.UI_Text_Delete_Word,
            UIActions.UI_Text_Delete_Word_Macos,
            UIActions.UI_Text_Delete_All_To_Right,
            UIActions.UI_Text_Delete_All_To_Right_Macos,
            UIActions.UI_Text_Caret_Left,
            UIActions.UI_Text_Caret_Word_Left,
            UIActions.UI_Text_Caret_Word_Left_Macos,
            UIActions.UI_Text_Caret_Right,
            UIActions.UI_Text_Caret_Word_Right,
            UIActions.UI_Text_Caret_Word_Right_Macos,
            UIActions.UI_Text_Caret_Up,
            UIActions.UI_Text_Caret_Down,
            UIActions.UI_Text_Caret_Line_Start,
            UIActions.UI_Text_Caret_Line_Start_Macos,
            UIActions.UI_Text_Caret_Line_End,
            UIActions.UI_Text_Caret_Line_End_Macos,
            UIActions.UI_Text_Caret_Page_Up,
            UIActions.UI_Text_Caret_Page_Down,
            UIActions.UI_Text_Caret_Document_Start,
            UIActions.UI_Text_Caret_Document_Start_Macos,
            UIActions.UI_Text_Caret_Document_End,
            UIActions.UI_Text_Caret_Document_End_Macos,
            UIActions.UI_Text_Caret_Add_Below,
            UIActions.UI_Text_Caret_Add_Below_Macos,
            UIActions.UI_Text_Caret_Add_Above,
            UIActions.UI_Text_Caret_Add_Above_Macos,
            UIActions.UI_Text_Scroll_Up,
            UIActions.UI_Text_Scroll_Up_Macos,
            UIActions.UI_Text_Scroll_Down,
            UIActions.UI_Text_Scroll_Down_Macos,
            UIActions.UI_Text_Select_All,
            UIActions.UI_Text_Select_Word_Under_Caret,
            UIActions.UI_Text_Select_Word_Under_Caret_Macos,
            UIActions.UI_Text_Add_Selection_For_Next_Occurrence,
            UIActions.UI_Text_Clear_Carets_And_Selection,
            UIActions.UI_Text_Toggle_Insert_Mode,
            UIActions.UI_Menu,
            UIActions.UI_Text_Submit,
            UIActions.UI_Graph_Duplicate,
            UIActions.UI_Graph_Delete,
            UIActions.UI_Filedialog_Up_One_Level,
            UIActions.UI_Filedialog_Refresh,
            UIActions.UI_Filedialog_Show_Hidden,
            UIActions.UI_Swap_Input_Direction,
        };
}
