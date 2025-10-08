//
// XTextFormatter.cs
//
// Authors:
//   Stefan Lange
//
// Copyright (c) 2005-2016 empira Software GmbH
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Globalization;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace TEKMET // Kendi projenizin Namespace'i ile aynı olduğundan emin olun
{
    /// <summary>
    /// Represents a formatted text.
    /// </summary>
    public class XTextFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XTextFormatter"/> class.
        /// </summary>
        public XTextFormatter(XGraphics gfx)
        {
            if (gfx == null)
                throw new ArgumentNullException("gfx");
            _gfx = gfx;
            _text = String.Empty;
            _font = new XFont("Times New Roman", 10);
            _brush = XBrushes.Black;
            _alignment = XParagraphAlignment.Left;
        }

        private readonly XGraphics _gfx;
        private string _text;
        private XFont _font;
        private XBrush _brush;
        private XParagraphAlignment _alignment;
        private XRect _rect;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public XFont Font
        {
            get { return _font; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("font");
                _font = value;
            }
        }

        /// <summary>
        /// Gets or sets the brush.
        /// </summary>
        public XBrush Brush
        {
            get { return _brush; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("brush");
                _brush = value;
            }
        }

        /// <summary>
        /// Gets or sets the alignment of the text.
        /// </summary>
        public XParagraphAlignment Alignment
        {
            get { return _alignment; }
            set { _alignment = value; }
        }

        /// <summary>
        /// Sets the rectangle within the text is drawn.
        /// </summary>
        public XRect Rectangle
        {
            get { return _rect; }
            set { _rect = value; }
        }

        /// <summary>
        /// Draws the text.
        /// </summary>
        public void DrawString(string text, XFont font, XBrush brush, XRect layoutRectangle, XStringFormat format)
        {
            _text = text;
            _font = font;
            _brush = brush;
            _rect = layoutRectangle;
            // HACK: just keep the horizontal alignment
            switch (format.Alignment)
            {
                case XStringAlignment.Near:
                    _alignment = XParagraphAlignment.Left;
                    break;
                case XStringAlignment.Center:
                    _alignment = XParagraphAlignment.Center;
                    break;

                case XStringAlignment.Far:
                    _alignment = XParagraphAlignment.Right;
                    break;
            }
            DrawString();
        }

        /// <summary>
        /// Draws the text.
        /// </summary>
        public void DrawString()
        {
            if (_text == null)
                return;

            double x = _rect.X;
            double lineSpace = _font.GetHeight();
            double cyAscent = lineSpace * _font.CellAscent / _font.CellSpace;
            double cyDescent = lineSpace * _font.CellDescent / _font.CellSpace;
            double y = _rect.Y + cyAscent;

            // Take all words of the text
            string[] words = _text.Split(' ');

            int count = words.Length;
            int wordIndex = 0;
            while (wordIndex < count)
            {
                // Format a single line
                string line = words[wordIndex++];
                while (wordIndex < count)
                {
                    string nextWord = words[wordIndex];
                    if (_gfx.MeasureString(line + " " + nextWord, _font).Width > _rect.Width)
                        break;
                    line += " " + nextWord;
                    wordIndex++;
                }

                // Draw the line
                double width = _gfx.MeasureString(line, _font).Width;
                switch (_alignment)
                {
                    case XParagraphAlignment.Left:
                        x = _rect.X;
                        break;

                    case XParagraphAlignment.Center:
                        x = _rect.X + (_rect.Width - width) / 2;
                        break;

                    case XParagraphAlignment.Right:
                        x = _rect.X + _rect.Width - width;
                        break;

                    case XParagraphAlignment.Justify:
                        // TODO: Justification is not yet implemented
                        x = _rect.X;
                        break;
                }
                _gfx.DrawString(line, _font, _brush, x, y);
                y += lineSpace;

                // Check if we are out of the rectangle
                if (y > _rect.Y + _rect.Height)
                    break;
            }
            LastRectangle = new XRect(_rect.X, _rect.Y, _rect.Width, y - _rect.Y - cyAscent);
        }

        /// <summary>
        /// Gets the rectangle of the last drawn string.
        /// </summary>
        public XRect LastRectangle { get; private set; }
    }
}