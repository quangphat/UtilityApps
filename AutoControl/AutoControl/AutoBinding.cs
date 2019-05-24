using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoControl
{
    public static class AutoBinding
    {
        private static object GetPropertyValue(string fieldname, object obj)
        {
            string[] props = fieldname.Split('.');
            object val = null;
            if (props != null)
            {
                PropertyInfo propInfo = obj.GetType().GetProperty(props[0]);
                if (propInfo != null)
                {
                    val = propInfo.GetValue(obj, null);
                    if (val != null)
                    {
                        for (int i = 1; i < props.Length; i++)
                        {
                            propInfo = val.GetType().GetProperty(props[i]);
                            val = propInfo != null ? propInfo.GetValue(val, null) : null;
                        }
                        return val;
                    }
                }
            }
            return val;
        }
        private static void SetProperty(string compoundProperty, object target, object value)
        {
            if (value == null) return;
            string[] bits = compoundProperty.Split('.');
            for (int i = 0; i < bits.Length - 1; i++)
            {
                PropertyInfo propertyToGet = target.GetType().GetProperty(bits[i]);
                if (propertyToGet != null)
                    target = propertyToGet.GetValue(target, null);
                else
                    target = null;
            }
            if (target != null)
            {
                PropertyInfo propertyToSet = target.GetType().GetProperty(bits.Last());
                if (propertyToSet != null)
                {
                    Type u = Nullable.GetUnderlyingType(propertyToSet.PropertyType);
                    if (u != null)
                    {
                        var val = ChangeType(value, u);
                        if (val != null)
                            propertyToSet.SetValue(target, val, null);
                    }
                    else
                    {
                        var val = ChangeType(value, propertyToSet.PropertyType);
                        if (val != null)
                            propertyToSet.SetValue(target, val, null);
                    }
                }
            }
        }
        private static object ChangeType(object obj, Type t)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    if (t == typeof(int))
                    {
                        return (int)(0);
                    }
                    if (t == typeof(double))
                    {
                        return (double)(0);
                    }
                    if (t == typeof(decimal))
                    {
                        return (decimal)(0);
                    }
                    if (t == typeof(float))
                    {
                        return (float)(0);
                    }
                }
                if (t == typeof(Guid) && !string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return Guid.Parse(obj.ToString());
                }
                if (t.BaseType.Name == "Enum")
                {
                    return Enum.Parse(t, obj.ToString(), true);
                }
                return Convert.ChangeType(obj, t);
            }
            catch
            {
                obj = "";
                return ChangeType(obj, t);
            }
        }
        public static void ToForm(this Control controls, object obj)
        {
            if (obj == null) return;
            string ObjectName = obj.GetType().Name;
            string bindingname = "";
            object val = null;
            foreach (Control ctrl in controls.Controls)
            {
                if (ctrl is AutoTextBox)
                {
                    AutoTextBox textbox = ctrl as AutoTextBox;
                    if (textbox.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(textbox.BindingName)) continue;
                    bindingname = textbox.BindingName;
                    val = GetPropertyValue(bindingname, obj);
                    textbox.Text = val != null ? val.ToString() : "";
                    continue;
                }
                if (ctrl is AutoRichTextBox)
                {
                    AutoRichTextBox textbox = ctrl as AutoRichTextBox;
                    if (textbox.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(textbox.BindingName)) continue;
                    bindingname = textbox.BindingName;
                    val = GetPropertyValue(bindingname, obj);
                    textbox.Text = val != null ? val.ToString() : "";
                    continue;
                }
                if (ctrl is AutoMetroTextBox)
                {
                    AutoMetroTextBox textbox = ctrl as AutoMetroTextBox;
                    if (textbox.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(textbox.BindingName)) continue;
                    bindingname = textbox.BindingName;
                    val = GetPropertyValue(bindingname, obj);
                    textbox.Text = val != null ? val.ToString() : "";
                    continue;
                }
                if (ctrl is AutoDatetime)
                {
                    AutoDatetime dtp = ctrl as AutoDatetime;
                    if (dtp.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(dtp.BindingName)) continue;
                    bindingname = dtp.BindingName;
                    val = GetPropertyValue(bindingname, obj);
                    dtp.Value = val != null ? Convert.ToDateTime(val) : DateTime.Today.AddDays(-10);
                    continue;
                }
                if (ctrl is AutoSearchCombobox)
                {
                    AutoSearchCombobox cbb = ctrl as AutoSearchCombobox;
                    if (cbb.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(cbb.BindingName)) continue;
                    bindingname = cbb.BindingName;
                    val = GetPropertyValue(bindingname, obj);
                    if (cbb.GetSelectedText == true)
                        cbb.Text = val != null ? val.ToString() : "";
                    else
                        cbb.SelectedValue = val != null ? Convert.ToInt32(val) : 0;
                    continue;
                }
                if (ctrl is AutoMetroCheckBox)
                {
                    AutoMetroCheckBox cb = ctrl as AutoMetroCheckBox;
                    if (cb.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(cb.BindingName)) continue;
                    bindingname = cb.BindingName;
                    val = GetPropertyValue(bindingname, obj);
                    cb.Checked = val == null ? false : Convert.ToBoolean(val);
                    continue;
                }
                if (ctrl is AutoMetroRadio)
                {
                    AutoMetroRadio rd = ctrl as AutoMetroRadio;
                    if (rd.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(rd.BindingName)) continue;
                    bindingname = rd.BindingName;
                    val = GetPropertyValue(bindingname, obj);
                    rd.Checked = (Convert.ToInt32((int)val) == rd.ValueToCheck) ? true : false;
                    continue;
                }
                if (ctrl.Controls.Count > 0)
                {
                    ToForm(ctrl, obj);
                }
            }
        }
        public static void ToEntity(this Control controls, object obj)
        {
            string ObjectName = obj.GetType().Name;
            string bindingname = "";
            foreach (Control ctrl in controls.Controls)
            {
                if (ctrl is AutoTextBox)
                {
                    AutoTextBox textbox = ctrl as AutoTextBox;
                    if (textbox.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(textbox.BindingName)) continue;
                    bindingname = textbox.BindingName;
                    SetProperty(bindingname, obj, textbox.Text);
                    continue;
                }
                if (ctrl is AutoRichTextBox)
                {
                    AutoRichTextBox textbox = ctrl as AutoRichTextBox;
                    if (textbox.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(textbox.BindingName)) continue;
                    bindingname = textbox.BindingName;
                    SetProperty(bindingname, obj, textbox.Text);
                    continue;
                }
                if (ctrl is AutoMetroTextBox)
                {
                    AutoMetroTextBox textbox = ctrl as AutoMetroTextBox;
                    if (textbox.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(textbox.BindingName)) continue;
                    bindingname = textbox.BindingName;
                    SetProperty(bindingname, obj, textbox.Text);
                    continue;
                }
                if (ctrl is AutoSearchCombobox)
                {
                    AutoSearchCombobox combobox = ctrl as AutoSearchCombobox;
                    if (combobox.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(combobox.BindingName)) continue;
                    bindingname = combobox.BindingName;
                    if (combobox.GetSelectedText == true)
                        SetProperty(bindingname, obj, combobox.Text);
                    else
                        SetProperty(bindingname, obj, combobox.SelectedValue);
                    continue;
                }
                if (ctrl is AutoDatetime)
                {
                    AutoDatetime dtp = ctrl as AutoDatetime;
                    if (dtp.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(dtp.BindingName)) continue;
                    bindingname = dtp.BindingName;
                    SetProperty(bindingname, obj, dtp.Value);
                    continue;
                }

                if (ctrl is AutoMetroCheckBox)
                {
                    AutoMetroCheckBox cb = ctrl as AutoMetroCheckBox;
                    if (cb.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(cb.BindingName)) continue;
                    bindingname = cb.BindingName;
                    SetProperty(bindingname, obj, cb.Checked);
                    continue;
                }
                if (ctrl is AutoMetroRadio)
                {
                    AutoMetroRadio rd = ctrl as AutoMetroRadio;
                    if (rd.BindingFor != ObjectName) continue;
                    if (string.IsNullOrWhiteSpace(rd.BindingName)) continue;
                    bindingname = rd.BindingName;
                    if (rd.Checked)
                    {
                        SetProperty(bindingname, obj, rd.ValueToCheck);
                    }
                    continue;
                }
                if (ctrl.Controls.Count > 0)
                {
                    ToEntity(ctrl, obj);
                }
            }
        }
    }
}
