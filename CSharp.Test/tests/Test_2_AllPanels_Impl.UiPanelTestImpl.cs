using DEYU.GDUtilities.AdpUiManagementSystem.Panels;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public partial class Test_2_AllPanels_Impl
{
    public partial class UiPanelTestImpl : UiPanel
    {
        protected override void OnPanelOpen() 
        {
            TestHelpers.Log("UiPanelTestImpl::OnPanelOpen");
            ClosePanel();
        }
    }

    public partial class UiPanelExternTestImpl : UiPanelExtern
    {
        protected override void OnPanelOpen() 
        {
            TestHelpers.Log("UiPanelExternTestImpl::OnPanelOpen");
            ClosePanel();
        }
    }

    public partial class UiPanelParamTestImpl : UiPanelParam<string, string>
    {
        protected override void OnPanelOpen(string openParam) 
        {
            TestHelpers.Log($"UiPanelParamTestImpl::OnPanelOpen({openParam})");
            ClosePanel("UiPanelParamTestImpl::ClosePanel");
        }
    }

    public partial class UiPanelParamOpenTestImpl : UiPanelParamOpen<string>
    {
        protected override void OnPanelOpen(string openParam) 
        {
            TestHelpers.Log($"UiP$anelParamOpenTestImpl::OnPanelOpen({openParam})");
            ClosePanel();
        }
    }

    public partial class UiPanelParamCloseTestImpl : UiPanelParamClose<string>
    {
        protected override void OnPanelOpen() 
        {
            TestHelpers.Log("UiPanelParamCloseTestImpl::OnPanelOpen");
            ClosePanel("UiPanelParamCloseTestImpl::ClosePanel");
        }
    }

    public partial class UiPanelParamExternOpenTestImpl : UiPanelParamExternOpen<string>
    {
        protected override void OnPanelOpen(string openParam) 
        {
            TestHelpers.Log($"UiPanelParamExternOpenTestImpl::OnPanelOpen({openParam})");
            ClosePanel();
        }
    }
    
    public partial class UiPanelExternTestAltImpl : UiPanelExtern
    {
        protected override void OnPanelOpen() => 
            TestHelpers.Log("UiPanelExternTestAltImpl::OnPanelOpen");
    }
    
    public partial class UiPanelParamExternOpenTestAltImpl : UiPanelParamExternOpen<string>
    {
        protected override void OnPanelOpen(string openParam) => 
            TestHelpers.Log($"UiPanelParamExternOpenTestAltImpl::OnPanelOpen({openParam})");
    }


}
