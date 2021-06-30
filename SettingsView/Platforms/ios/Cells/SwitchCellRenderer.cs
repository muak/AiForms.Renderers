using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using AiSwitchCellRenderer = AiForms.Renderers.iOS.SwitchCellRenderer;
using AiSwitchCell = AiForms.Renderers.SwitchCell;
using Foundation;

[assembly: ExportRenderer(typeof(AiSwitchCell), typeof(AiSwitchCellRenderer))]
namespace AiForms.Renderers.iOS
{
    /// <summary>
    /// Switch cell renderer.
    /// </summary>
    [Foundation.Preserve(AllMembers = true)]
    public class SwitchCellRenderer : CellBaseRenderer<SwitchCellView> { }

    /// <summary>
    /// Switch cell view.
    /// </summary>
    [Foundation.Preserve(AllMembers = true)]
    public class SwitchCellView : CellBaseView
    {
        
        AiSwitchCell _SwitchCell => Cell as AiSwitchCell;
        UISwitch _switch;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AiForms.Renderers.iOS.SwitchCellView"/> class.
        /// </summary>
        /// <param name="formsCell">Forms cell.</param>
        public SwitchCellView(Cell formsCell) : base(formsCell)
        {

            _switch = new UISwitch();
            _switch.ValueChanged += _switch_ValueChanged;

            this.AccessoryView = _switch;
            EditingAccessoryView = _switch;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);
            _switch.On = !_switch.On;
        }
        /// <summary>
        /// Cells the property changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public override void CellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.CellPropertyChanged(sender, e);
            if (e.PropertyName == AiSwitchCell.AccentColorProperty.PropertyName) {
                UpdateAccentColor();
            }
            if (e.PropertyName == AiSwitchCell.OnProperty.PropertyName) {
                UpdateOn();
            }
        }

        /// <summary>
        /// Parents the property changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public override void ParentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.ParentPropertyChanged(sender, e);
            if (e.PropertyName == SettingsView.CellAccentColorProperty.PropertyName) {
                UpdateAccentColor();
            }
        }

        /// <summary>
        /// Updates the cell.
        /// </summary>
        public override void UpdateCell(UITableView tableView)
        {
            base.UpdateCell(tableView);
            if (_switch is null)
                return; // for HotReload

            UpdateAccentColor();
            UpdateOn();
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <returns>The dispose.</returns>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _switch.ValueChanged -= _switch_ValueChanged;
                AccessoryView = null;
                _switch?.Dispose();
                _switch = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Sets the enabled appearance.
        /// </summary>
        /// <param name="isEnabled">If set to <c>true</c> is enabled.</param>
        protected override void SetEnabledAppearance(bool isEnabled)
        {
            if(isEnabled){
                _switch.Alpha = 1.0f;
            }
            else{
                _switch.Alpha = 0.3f;
            }
            base.SetEnabledAppearance(isEnabled);
        }

        void _switch_ValueChanged(object sender, EventArgs e)
        {
            _SwitchCell.On = _switch.On;
        }

        void UpdateOn()
        {
            if (_switch.On != _SwitchCell.On) {
                _switch.On = _SwitchCell.On;
            }
        }

        void UpdateAccentColor()
        {
            if (_SwitchCell.AccentColor != Xamarin.Forms.Color.Default) {
                _switch.OnTintColor = _SwitchCell.AccentColor.ToUIColor();
            }
            else if (CellParent != null && CellParent.CellAccentColor != Xamarin.Forms.Color.Default) {
                _switch.OnTintColor = CellParent.CellAccentColor.ToUIColor();
            }
        }

    }
}
