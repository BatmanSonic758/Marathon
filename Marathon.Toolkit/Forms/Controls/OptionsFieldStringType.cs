﻿// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Reflection;

namespace Marathon.Toolkit.Controls
{
    public partial class OptionsFieldStringType : OptionsField
    {
        private string _OptionName, _OptionDescription, _OptionField;
        private PropertyInfo _OptionProperty;

        /// <summary>
        /// The name of the option.
        /// </summary>
        public override string OptionName
        {
            get => _OptionName;

            set => Label_Title.Text = _OptionName = value;
        }

        /// <summary>
        /// The description given to the option.
        /// </summary>
        public override string OptionDescription
        {
            get => _OptionDescription;

            set => Label_Description.Text = _OptionDescription = value;
        }

        /// <summary>
        /// The property assigned to this option.
        /// </summary>
        public override PropertyInfo OptionProperty
        {
            get => _OptionProperty;

            set
            {
                _OptionProperty = value;

                if (_OptionProperty != null)
                    TextBox_String.Text = (string)_OptionProperty.GetValue(value);
            }
        }

        /// <summary>
        /// The string assigned to this option.
        /// </summary>
        private string OptionField
        {
            get => _OptionField;

            set
            {
                _OptionField = value;

                if (OptionProperty != null)
                    OptionProperty.SetValue(OptionProperty, value);
            }
        }

        public OptionsFieldStringType() => InitializeComponent();

        /// <summary>
        /// Sets the property to the current text.
        /// </summary>
        private void TextBox_String_TextChanged(object sender, EventArgs e) => OptionField = TextBox_String.Text;
    }
}
