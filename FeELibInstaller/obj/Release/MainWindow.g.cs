﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "56D0B1C187DA5670FBB7107FCA03FA1879FFFBA9CE2884105C6BA92F032F6737"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FeELibInstaller;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FeELibInstaller {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 197 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Help_Button;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close_Button;
        
        #line default
        #line hidden
        
        
        #line 220 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Drag_Border;
        
        #line default
        #line hidden
        
        
        #line 224 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Notify_TextBlock;
        
        #line default
        #line hidden
        
        
        #line 240 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NotifyFile_TextBlock;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FilePath_TextBlock;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NotifyTargetFile_TextBlock;
        
        #line default
        #line hidden
        
        
        #line 252 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FileTargetPath_TextBlock;
        
        #line default
        #line hidden
        
        
        #line 261 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid VersionSelector_Grid;
        
        #line default
        #line hidden
        
        
        #line 269 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Version_StackPanel;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Start_Button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FeELibInstaller;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\MainWindow.xaml"
            ((FeELibInstaller.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Help_Button = ((System.Windows.Controls.Button)(target));
            
            #line 199 "..\..\MainWindow.xaml"
            this.Help_Button.Click += new System.Windows.RoutedEventHandler(this.Help_Button_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Close_Button = ((System.Windows.Controls.Button)(target));
            
            #line 208 "..\..\MainWindow.xaml"
            this.Close_Button.Click += new System.Windows.RoutedEventHandler(this.Close_Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Drag_Border = ((System.Windows.Controls.Border)(target));
            
            #line 221 "..\..\MainWindow.xaml"
            this.Drag_Border.Drop += new System.Windows.DragEventHandler(this.Drag_Border_Drop);
            
            #line default
            #line hidden
            
            #line 221 "..\..\MainWindow.xaml"
            this.Drag_Border.PreviewDragOver += new System.Windows.DragEventHandler(this.Drag_Border_PreviewDragOver);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Notify_TextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.NotifyFile_TextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.FilePath_TextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.NotifyTargetFile_TextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.FileTargetPath_TextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.VersionSelector_Grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.Version_StackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 12:
            this.Start_Button = ((System.Windows.Controls.Button)(target));
            
            #line 290 "..\..\MainWindow.xaml"
            this.Start_Button.Click += new System.Windows.RoutedEventHandler(this.Start_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
