using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Resources;

namespace System.Globalization 
{
	public class FormLanguageSwitchSingleton 
	{
		/// <summary>
		///     Static read-only instance.
		/// </summary>
		protected static readonly FormLanguageSwitchSingleton m_instance = new FormLanguageSwitchSingleton();
        
		/// <summary>
		///     Hidden constructor called during static member <c>m_instance</c> 
		///     initialization.
		/// </summary>
		protected FormLanguageSwitchSingleton() { }

		/// <summary>
		///     Gets the only instance of the object.
		/// </summary>
		public static FormLanguageSwitchSingleton Instance 
		{
			get { return m_instance; }
		}

		/// <summary>
		///     Changes the current culture used by the Resource Manager to look 
		///     up culture-specific resources at run time.
		/// </summary>
		/// <param name="cultureInfoIdentifier">
		///     A <c>CultureInfo</c> object that will be applied to the 
		///     application thread.
		/// </param>
		public void ChangeCurrentThreadUICulture(System.Globalization.CultureInfo cultureInfo) 
		{
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}

		/// <summary>
		///     Changes the language of the <c>Form</c> object provided to the 
		///     currently selected language.
		/// </summary>
		/// <param name="form">
		///     <c>Form</c> object to apply changes to.
		/// </param>
		public void ChangeLanguage(System.Windows.Forms.Form form) 
		{
			ChangeLanguage(form, Thread.CurrentThread.CurrentUICulture);
		}

		/// <summary>
		///     Changes the language of the <c>Form</c> object provided and all 
		///     its MDI children (in the case of MDI UI) to the currently 
		///     selected language.
		/// </summary>
		/// <param name="form">
		///     <c>Form</c> object to apply changes to.
		/// </param>
		/// <param name="cultureInfo">
		///     <c>CultureInfo</c> to which language has to be changed.
		/// </param>
		public void ChangeLanguage(System.Windows.Forms.Form form, System.Globalization.CultureInfo cultureInfo) 
		{
			m_cultureInfo = cultureInfo;
			ChangeFormLanguage(form);
			foreach (System.Windows.Forms.Form childForm in form.MdiChildren) 
			{
				ChangeFormLanguage(childForm);
			}
		}

		/// <summary>
		///     Changes <c>Text</c> properties associated with following 
		///     controls: <c>AxHost</c>, <c>ButtonBase</c>, <c>GroupBox</c>, 
		///     <c>Label</c>, <c>ScrollableControl</c>, <c>StatusBar</c>, 
		///     <c>TabControl</c>, <c>ToolBar</c>. Method is made virtual so it 
		///     can be overriden in derived class to redefine types.
		/// </summary>
		/// <param name="parent">
		///     <c>Control</c> object.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		protected virtual void ReloadTextForSelectedControls(System.Windows.Forms.Control control, System.Resources.ResourceManager resources) 
		{
			if (control is System.Windows.Forms.AxHost ||
				control is System.Windows.Forms.ButtonBase ||
				control is System.Windows.Forms.GroupBox ||
				control is System.Windows.Forms.Label ||
				control is System.Windows.Forms.ScrollableControl ||
				control is System.Windows.Forms.StatusBar ||
				control is System.Windows.Forms.TabControl ||
				control is System.Windows.Forms.ToolBar) 
			{
				control.Text = resources.GetString(control.Name + ".Text", m_cultureInfo);
			}
		}

		/// <summary>
		///     Reloads properties common to all controls (except the <c>Text</c>
		///     property).
		/// </summary>
		/// <param name="control">
		///     <c>Control</c> object for which resources should be reloaded.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		protected virtual void ReloadControlCommonProperties(System.Windows.Forms.Control control, System.Resources.ResourceManager resources) 
		{
			SetProperty(control, "AccessibleDescription", resources);
			SetProperty(control, "AccessibleName", resources);
			SetProperty(control, "BackgroundImage", resources);
			SetProperty(control, "Font", resources);
			SetProperty(control, "ImeMode", resources);
			SetProperty(control, "RightToLeft", resources);
			SetProperty(control, "Size", resources);
			// following properties are not changed for the form
			if (!(control is System.Windows.Forms.Form)) 
			{
				SetProperty(control, "Anchor", resources);
				SetProperty(control, "Dock", resources);
				SetProperty(control, "Enabled", resources);
				SetProperty(control, "Location", resources);
				SetProperty(control, "TabIndex", resources);
				SetProperty(control, "Visible", resources);
			}
			if (control is System.Windows.Forms.ScrollableControl) 
			{
				ReloadScrollableControlProperties((System.Windows.Forms.ScrollableControl)control, resources);
				if (control is System.Windows.Forms.Form) 
				{
					ReloadFormProperties((System.Windows.Forms.Form)control, resources);
				}
			}
		}

		/// <summary>
		///     Reloads properties specific to some controls.
		/// </summary>
		/// <param name="control">
		///     <c>Control</c> object for which resources should be reloaded.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		protected virtual void ReloadControlSpecificProperties(System.Windows.Forms.Control control, System.Resources.ResourceManager resources) 
		{
			// ImageIndex property for ButtonBase, Label, TabPage, ToolBarButton, TreeNode, TreeView
			SetProperty(control, "ImageIndex", resources);
			// ToolTipText property for StatusBar, TabPage, ToolBarButton
			SetProperty(control, "ToolTipText", resources);
			// IntegralHeight property for ComboBox, ListBox
			SetProperty(control, "IntegralHeight", resources);
			// ItemHeight property for ListBox, ComboBox, TreeView
			SetProperty(control, "ItemHeight", resources);
			// MaxDropDownItems property for ComboBox
			SetProperty(control, "MaxDropDownItems", resources);
			// MaxLength property for ComboBox, RichTextBox, TextBoxBase
			SetProperty(control, "MaxLength", resources);
			// Appearance property for CheckBox, RadioButton, TabControl, ToolBar
			SetProperty(control, "Appearance", resources);
			// CheckAlign property for CheckBox and RadioBox
			SetProperty(control, "CheckAlign", resources);
			// FlatStyle property for ButtonBase, GroupBox and Label
			SetProperty(control, "FlatStyle", resources);
			// ImageAlign property for ButtonBase, Image and Label
			SetProperty(control, "ImageAlign", resources);
			// Indent property for TreeView
			SetProperty(control, "Indent", resources);
			// Multiline property for RichTextBox, TabControl, TextBoxBase
			SetProperty(control, "Multiline", resources);
			// BulletIndent property for RichTextBox
			SetProperty(control, "BulletIndent", resources);
			// RightMargin property for RichTextBox
			SetProperty(control, "RightMargin", resources);
			// ScrollBars property for RichTextBox, TextBox
			SetProperty(control, "ScrollBars", resources);
			// WordWrap property for TextBoxBase
			SetProperty(control, "WordWrap", resources);
			// ZoomFactor property for RichTextBox
			SetProperty(control, "ZoomFactor", resources);
		}

		/// <summary>
		///     Scans controls that are not contained by <c>Controls</c> 
		///     collection, like <c>MenuItem</c>s, <c>StatusBarPanel</c>s
		///     and <c>ColumnHeader</c>s.
		/// </summary>
		/// <param name="form">
		///     <c>Form</c> object to scan.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> used to get localized resources.
		/// </param>
		protected virtual void ScanNonControls(System.Windows.Forms.Form form, System.Resources.ResourceManager resources) 
		{
			FieldInfo[] fieldInfo = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fieldInfo.Length; i++) 
			{
				object o, obj = fieldInfo[i].GetValue(form);
				string fieldName = fieldInfo[i].Name;
				if (obj is System.Windows.Forms.MenuItem) 
				{
					System.Windows.Forms.MenuItem menuItem = (System.Windows.Forms.MenuItem)obj;
					menuItem.Enabled = (o = resources.GetObject(fieldName + ".Enabled", m_cultureInfo)) != null ? (bool) o : menuItem.Enabled;
					menuItem.Shortcut = (o = resources.GetObject(fieldName + ".Shortcut", m_cultureInfo)) != null ? (Shortcut) o : menuItem.Shortcut;
					menuItem.ShowShortcut = (o = resources.GetObject(fieldName + ".ShowShortcut", m_cultureInfo)) != null ? (bool) o : menuItem.ShowShortcut;
					menuItem.Text = resources.GetString(fieldName + ".Text", m_cultureInfo);
					menuItem.Visible = (o = resources.GetObject(fieldName + ".Visible", m_cultureInfo)) != null ? (bool) o : menuItem.Visible;
				}
				if (obj is System.Windows.Forms.StatusBarPanel) 
				{
					System.Windows.Forms.StatusBarPanel panel = (System.Windows.Forms.StatusBarPanel)obj;
					panel.Alignment = (o = resources.GetObject(fieldName + ".Alignment", m_cultureInfo)) != null ? (HorizontalAlignment) o : panel.Alignment;
					panel.Icon = (o = resources.GetObject(fieldName + ".Icon", m_cultureInfo)) != null ? (Icon) o : panel.Icon;
					panel.MinWidth = (o = resources.GetObject(fieldName + ".MinWidth", m_cultureInfo)) != null ? (int) o : panel.MinWidth;
					panel.Text = resources.GetString(fieldName + ".Text", m_cultureInfo);
					panel.ToolTipText = resources.GetString(fieldName + ".ToolTipText", m_cultureInfo);
					panel.Width = (o = resources.GetObject(fieldName + ".Width", m_cultureInfo)) != null ? (int) o : panel.Width;
				}
				if (obj is System.Windows.Forms.ColumnHeader) 
				{
					System.Windows.Forms.ColumnHeader header = (System.Windows.Forms.ColumnHeader)obj;
					header.Text = resources.GetString(fieldName + ".Text", m_cultureInfo);
					header.TextAlign = (o = resources.GetObject(fieldName + ".TextAlign", m_cultureInfo)) != null ? (HorizontalAlignment) o : header.TextAlign;
					header.Width = (o = resources.GetObject(fieldName + ".Width", m_cultureInfo)) != null ? (int) o : header.Width;
				}
				if (obj is System.Windows.Forms.ToolBarButton) 
				{
					System.Windows.Forms.ToolBarButton button = (System.Windows.Forms.ToolBarButton)obj;
					button.Enabled = (o = resources.GetObject(fieldName + ".Enabled", m_cultureInfo)) != null ? (bool) o : button.Enabled;
					button.ImageIndex = (o = resources.GetObject(fieldName + ".ImageIndex", m_cultureInfo)) != null ? (int) o : button.ImageIndex;
					button.Text = resources.GetString(fieldName + ".Text", m_cultureInfo);
					button.ToolTipText = resources.GetString(fieldName + ".ToolTipText", m_cultureInfo);
					button.Visible = (o = resources.GetObject(fieldName + ".Visible", m_cultureInfo)) != null ? (bool) o : button.Visible;
				}
			}
		}

		/// <summary>
		///     Reloads items in following controls: <c>ComboBox</c>, 
		///     <c>ListBox</c>, <c>DomainUpDown</c>. Method is made virtual so 
		///     it can be overriden in derived class to redefine types.
		/// </summary>
		/// <param name="parent">
		///     <c>Control</c> object.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		protected virtual void ReloadListItems(System.Windows.Forms.Control control, System.Resources.ResourceManager resources) 
		{
			if (control is System.Windows.Forms.ComboBox) 
				ReloadComboBoxItems((System.Windows.Forms.ComboBox)control, resources);
			else if (control is System.Windows.Forms.ListBox) 
				ReloadListBoxItems((System.Windows.Forms.ListBox)control, resources);
			else if (control is System.Windows.Forms.DomainUpDown)
				ReloadUpDownItems((System.Windows.Forms.DomainUpDown)control, resources);
		}

		/// <summary>
		///     Changes the language of the form.
		/// </summary>
		/// <param name="form">
		///     <c>Form</c> object to apply changes to. 
		/// </param>
		private void ChangeFormLanguage(System.Windows.Forms.Form form) 
		{
			form.SuspendLayout();
			Cursor.Current = Cursors.WaitCursor;
			ResourceManager resources = new System.Resources.ResourceManager(form.GetType());
			// change main form resources
			form.Text = resources.GetString("$this.Text", m_cultureInfo);
			ReloadControlCommonProperties(form, resources);
			ToolTip toolTip = GetToolTip(form);
			// change text of all containing controls
			RecurControls(form, resources, toolTip);
			// change the text of menus
			ScanNonControls(form, resources);
			form.ResumeLayout();
		}

		/// <summary>
		///     Gets the <c>ToolTip</c> member of the control (<c>Form</c> or 
		///     <c>UserControl</c>).
		/// </summary>
		/// <param name="control">
		///     <c>Control</c> for which tooltip is requested.
		/// </param>
		/// <returns>
		///     <c>ToolTip</c> of the control or <c>null</c> if not defined.
		/// </returns>
		private ToolTip GetToolTip(System.Windows.Forms.Control control) 
		{
			Debug.Assert(control is System.Windows.Forms.Form || control is System.Windows.Forms.UserControl);
			FieldInfo[] fieldInfo = control.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fieldInfo.Length; i++) 
			{
				object obj = fieldInfo[i].GetValue(control);
				if (obj is System.Windows.Forms.ToolTip) 
					return (ToolTip)obj;
			}
			return null;
		}

		/// <summary>
		///     Recurs <c>Controls</c> members of the control to change 
		///     corresponding texts.
		/// </summary>
		/// <param name="parent">
		///     Parent <c>Control</c> object.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void RecurControls(System.Windows.Forms.Control parent, System.Resources.ResourceManager resources, System.Windows.Forms.ToolTip toolTip) 
		{
			foreach (Control control in parent.Controls) 
			{
				ReloadControlCommonProperties(control, resources);
				ReloadControlSpecificProperties(control, resources);
				if (toolTip != null)
					toolTip.SetToolTip(control, resources.GetString(control.Name + ".ToolTip", m_cultureInfo));
				if (control is System.Windows.Forms.UserControl)
					RecurUserControl((System.Windows.Forms.UserControl)control);
				else 
				{
					ReloadTextForSelectedControls(control, resources);
					ReloadListItems(control, resources);
					if (control is System.Windows.Forms.TreeView) 
						ReloadTreeViewNodes((System.Windows.Forms.TreeView)control, resources);
					if (control.Controls.Count > 0)
						RecurControls(control, resources, toolTip);
				}
			}
		}

		/// <summary>
		///     Reloads resources specific to the <c>Form</c> type.
		/// </summary>
		/// <param name="form">
		///     <c>Form</c> object to apply changes to.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void ReloadFormProperties(System.Windows.Forms.Form form, System.Resources.ResourceManager resources) 
		{
			SetProperty(form, "AutoScaleBaseSize", resources);
			SetProperty(form, "Icon", resources);
			SetProperty(form, "MaximumSize", resources);
			SetProperty(form, "MinimumSize", resources);
		}

		/// <summary>
		///     Reloads resources specific to the <c>ScrollableControl</c> type.
		/// </summary>
		/// <param name="control">
		///     <c>Control</c> object for which resources should be reloaded.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void ReloadScrollableControlProperties(System.Windows.Forms.ScrollableControl control, System.Resources.ResourceManager resources) 
		{
			SetProperty(control, "AutoScroll", resources);
			SetProperty(control, "AutoScrollMargin", resources);
			SetProperty(control, "AutoScrollMinSize", resources);
			SetProperty(control, "AutoScrollPosition", resources);
		}

		/// <summary>
		///     Reloads resources for a property.
		/// </summary>
		/// <param name="control">
		///     <c>Control</c> object for which resources should be reloaded.
		/// </param>
		/// <param name="propertyName">
		///     Name of the property to reload.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void SetProperty(System.Windows.Forms.Control control, string propertyName, System.Resources.ResourceManager resources) 
		{
			PropertyInfo propertyInfo = control.GetType().GetProperty(propertyName);
			if (propertyInfo != null) 
			{
				string controlName = control.Name;
				if (control is System.Windows.Forms.Form)
					controlName = "$this";
				object resObject = resources.GetObject(controlName + "." + propertyName, m_cultureInfo);
				if (resObject != null) 
					propertyInfo.SetValue(control, Convert.ChangeType(resObject, propertyInfo.PropertyType), null);
			}
		}
        
		/// <summary>
		///     Recurs <c>UserControl</c> to change.
		/// </summary>
		/// <param name="parent">
		///     <c>UserControl</c> object to scan.
		/// </param>
		private void RecurUserControl(System.Windows.Forms.UserControl userControl) 
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(userControl.GetType());
			ToolTip toolTip = GetToolTip(userControl);
			RecurControls(userControl, resources, toolTip);
		}

		/// <summary>
		///     Reloads items in the <c>ListBox</c>. If items are not sorted, 
		///     selections are kept.
		/// </summary>
		/// <param name="listBox">
		///     <c>ListBox</c> to localize.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void ReloadListBoxItems(System.Windows.Forms.ListBox listBox, System.Resources.ResourceManager resources) 
		{
			if (listBox.Items.Count > 0) 
			{
				int[] selectedItems = new int[listBox.SelectedIndices.Count];
				listBox.SelectedIndices.CopyTo(selectedItems, 0);
				ReloadItems(listBox.Name, listBox.Items, listBox.Items.Count, resources);
				if (!listBox.Sorted) 
				{
					for (int i = 0; i < selectedItems.Length; i++) 
					{
						listBox.SetSelected(selectedItems[i], true);
					}
				}
			}
		}

		/// <summary>
		///     Reloads items in the <c>ComboBox</c>. If items are not sorted, 
		///     selection is kept.
		/// </summary>
		/// <param name="listBox">
		///     <c>ComboBox</c> to localize.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void ReloadComboBoxItems(System.Windows.Forms.ComboBox comboBox, System.Resources.ResourceManager resources) 
		{
			if (comboBox.Items.Count > 0) 
			{
				int selectedIndex = comboBox.SelectedIndex;
				ReloadItems(comboBox.Name, comboBox.Items, comboBox.Items.Count, resources);
				if (!comboBox.Sorted)
					comboBox.SelectedIndex = selectedIndex;
			}
		}

		/// <summary>
		///     Reloads items in the <c>DomainUpDown</c> control. If items are 
		///     not sorted, selection is kept.
		/// </summary>
		/// <param name="listBox">
		///     <c>DomainUpDown</c> to localize.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void ReloadUpDownItems(System.Windows.Forms.DomainUpDown domainUpDown, System.Resources.ResourceManager resources) 
		{
			if (domainUpDown.Items.Count > 0) 
			{
				int selectedIndex = domainUpDown.SelectedIndex;
				ReloadItems(domainUpDown.Name, domainUpDown.Items, domainUpDown.Items.Count, resources);
				if (!domainUpDown.Sorted)
					domainUpDown.SelectedIndex = selectedIndex;
			}
		}

		/// <summary>
		///     Reloads content of a <c>TreeView</c>.
		/// </summary>
		/// <param name="treeView">
		///     <c>TreeView</c> control to reload.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void ReloadTreeViewNodes(System.Windows.Forms.TreeView treeView, System.Resources.ResourceManager resources) 
		{
			treeView.Nodes.Clear();
			object obj = resources.GetObject(treeView.Name + ".Nodes.Nodes", m_cultureInfo);
			if (obj != null)
				treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { (System.Windows.Forms.TreeNode)obj });
		}

		/// <summary>
		///     Clears all items in the <c>IList</c> and reloads the list with
		///     items according to language settings.
		/// </summary>
		/// <param name="controlName">
		///     Name of the control.
		/// </param>
		/// <param name="list">
		///     <c>IList</c> with items to change.
		/// </param>
		/// <param name="itemsNumber">
		///     Number of items.
		/// </param>
		/// <param name="resources">
		///     <c>ResourceManager</c> object.
		/// </param>
		private void ReloadItems(string controlName, IList list, int itemsNumber, System.Resources.ResourceManager resources) 
		{
			string resourceName = controlName + ".Items.Items";
			if (resources.GetString(resourceName, m_cultureInfo) == null) return;
			list.Clear();
			list.Add(resources.GetString(resourceName, m_cultureInfo));
			for (int i = 1; i < itemsNumber; i++) 
				list.Add(resources.GetString(resourceName + i, m_cultureInfo));
		}

		/// <summary>
		///     <c>CultureInfo</c> used by Resource Manager.
		/// </summary>
		private System.Globalization.CultureInfo m_cultureInfo;
	}
}