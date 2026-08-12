//  -*-  coding: utf-8-with-signature-unix;        -*-  //
/*************************************************************************
**                                                                      **
**                  ----   NES Debugger Project   ----                  **
**                                                                      **
**          Copyright (C), 2026-2026, Takahiro Itou                     **
**          All Rights Reserved.                                        **
**                                                                      **
**          License: (See COPYING or LICENSE files)                     **
**          GNU Affero General Public License (AGPL) version 3,         **
**          or (at your option) any later version.                      **
**                                                                      **
*************************************************************************/

using System.ComponentModel;
using System.Windows.Controls;

namespace  NesWpfControl.Views  {

public  partial class  GameScreen : UserControl
{

//----------------------------------------------------------------
/**   デフォルトコンストラクタ。
**
**/
public  GameScreen()
{
    InitializeComponent();
    m_imgBuffer = new System.Drawing.Bitmap(512, 480);
}


//========================================================================
//
//
//

//----------------------------------------------------------------
/**   画像をクリアする。
**
**/
public  virtual  void
clearScreen()
{
    System.Drawing.Bitmap   imgCanvas;
    System.Drawing.Graphics grpCanvas;

    //imgCanvas = new System.Drawing.Bitmap(picView.Width, picView.Height);
    imgCanvas = new System.Drawing.Bitmap(256, 240);
    grpCanvas = System.Drawing.Graphics.FromImage(imgCanvas);

    grpCanvas.FillRectangle(
            System.Drawing.Brushes.White, grpCanvas.VisibleClipBounds);
    grpCanvas.Dispose();

    //  picView.Image = imgCanvas;
}

//----------------------------------------------------------------
/**   デフォルトの描画処理を行う。
**
**/
public  virtual  void
drawScreen()
{
    this.m_wManPpu?.drawScreen();
}

//----------------------------------------------------------------
/**   画面を初期化する。
**
**/
public  virtual  System.Boolean
initializeScreenImage(int W, int H)
{
    IntPtr  hDC;
    System.Drawing.Graphics grpBuffer;

    grpBuffer = System.Drawing.Graphics.FromImage(m_imgBuffer);

    hDC = grpBuffer.GetHdc();
    if ( m_screenImage == null ) {
        m_screenImage = m_bitmapRenderer.createImage(hDC, W, H);
    }
    grpBuffer.ReleaseHdc(hDC);
    grpBuffer.Dispose();

    return true;
}

//----------------------------------------------------------------
/**   PPU を設定する。
**
**/
public  virtual  System.Boolean
setupPpuManager(NesDbgWrap.NesMan.BasePpuCore manPpu)
{
    if ( this.m_screenImage == null ) {
        return  false;
    }
    manPpu.TargetImage  = this.m_screenImage;
    this.m_wManPpu  = manPpu;

    return  true;
}

//----------------------------------------------------------------
/**   ゲーム画面を表示する。
**
**/
public  virtual  void
showScreen()
{
    System.Drawing.Bitmap   imgCanvas;
    System.Drawing.Graphics grpCanvas;
    IntPtr  hDC;
    System.Drawing.Brush    brushBG;
    System.Drawing.Color    colorBG;

    imgCanvas = this.m_imgBuffer;
    grpCanvas = System.Drawing.Graphics.FromImage(imgCanvas);

    colorBG = System.Drawing.Color.FromArgb(0xFF, 0x00, 0x00, 0xFF);
    brushBG = new System.Drawing.SolidBrush(colorBG);
    grpCanvas.FillRectangle(brushBG, grpCanvas.VisibleClipBounds);

    hDC = grpCanvas.GetHdc();
    m_bitmapRenderer.drawImage(hDC, 0, 0, 256, 240, 0, 0);
    grpCanvas.ReleaseHdc(hDC);
    grpCanvas.Dispose();

    //  picView.Image = imgCanvas;
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**   MarginAreaColor プロパティ
**
**/
[Browsable(true)
  , Description("余白部分の背景色")
  , Category("表示")
]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
public  System.Drawing.Color
MarginAreaColor
{
    get { return  this.m_marginColor; }
    set { this.m_marginColor = value; }
}

//----------------------------------------------------------------
/**   SourcePicture プロパティ
**
**/
[Browsable(true)
  , Description("描画領域")
  , Category("表示")
]
public  System.Windows.Controls.Image
SourcePicture
{
    get { return  this.picView; }
}


//========================================================================
//
//    Member Variables.
//

/**   イメージレンダラ。    **/
private NesDbgWrap.Images.BitmapRenderer    m_bitmapRenderer
    = new NesDbgWrap.Images.BitmapRenderer();

/**   PPU マネージャ。      **/
private NesDbgWrap.NesMan.BasePpuCore?      m_wManPpu;

/**   イメージ用バッファ。  **/
System.Drawing.Bitmap                       m_imgBuffer;

private NesDbgWrap.Images.FullColorImage?   m_screenImage;

private System.Drawing.Color    m_marginColor;

}   //  End class  GameScreen

}   //  End of namespace  NesWpfControl.Views
