using DEYU.GDUtilities.AdpUIManagementSystem.Panels;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_2_AllPanels_Impl
{
    public partial class UIPanelTestImpl : UIPanel
    {
        protected override void OnPanelOpen() 
        {
            TestHelpers.Log("UIPanelTestImpl::OnPanelOpen");
            ClosePanel();
        }
    }

    public partial class UIPanelExternTestImpl : UIPanelExtern
    {
        protected override void OnPanelOpen() 
        {
            TestHelpers.Log("UIPanelExternTestImpl::OnPanelOpen");
            ClosePanel();
        }
    }

    public partial class UIPanelParamTestImpl : UIPanelParam<string, string>
    {
        protected override void OnPanelOpen(string openParam) 
        {
            TestHelpers.Log($"UIPanelParamTestImpl::OnPanelOpen({openParam})");
            ClosePanel("UIPanelParamTestImpl::ClosePanel");
        }
    }

    public partial class UIPanelParamOpenTestImpl : UIPanelParamOpen<string>
    {
        protected override void OnPanelOpen(string openParam) 
        {
            TestHelpers.Log($"UIP$anelParamOpenTestImpl::OnPanelOpen({openParam})");
            ClosePanel();
        }
    }

    public partial class UIPanelParamCloseTestImpl : UIPanelParamClose<string>
    {
        protected override void OnPanelOpen() 
        {
            TestHelpers.Log("UIPanelParamCloseTestImpl::OnPanelOpen");
            ClosePanel("UIPanelParamCloseTestImpl::ClosePanel");
        }
    }

    public partial class UIPanelParamExternOpenTestImpl : UIPanelParamExternOpen<string>
    {
        protected override void OnPanelOpen(string openParam) 
        {
            TestHelpers.Log($"UIPanelParamExternOpenTestImpl::OnPanelOpen({openParam})");
            ClosePanel();
        }
    }
    
    public partial class UIPanelExternTestAltImpl : UIPanelExtern
    {
        protected override void OnPanelOpen() => 
            TestHelpers.Log("UIPanelExternTestAltImpl::OnPanelOpen");
    }
    
    public partial class UIPanelParamExternOpenTestAltImpl : UIPanelParamExternOpen<string>
    {
        protected override void OnPanelOpen(string openParam) => 
            TestHelpers.Log($"UIPanelParamExternOpenTestAltImpl::OnPanelOpen({openParam})");
    }


}
