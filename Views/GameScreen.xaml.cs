//  -*-  coding: utf-8-with-signature  -*-  //
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

using NesWpfControl.Models;
using NesWpfControl.ViewModels;

using NesPpuManager  = NesDbgWrap.NesMan.BasePpuCore;


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
//    Accessors.
//

//----------------------------------------------------------------
/**   ビューモデルを設定する。
**
**/

public  void
setViewModel(
        GameScreenViewModel viewModel)
{
    m_viewModel = viewModel;
    DataContext = viewModel;
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
    return ( true );
}

//----------------------------------------------------------------
/**   PPU を設定する。
**
**/
public  virtual  System.Boolean
setupPpuManager(NesDbgWrap.NesMan.BasePpuCore manPpu)
{
    RunningScreenViewModel  vm;

    this.m_ppuModel = new PpuModelManager(manPpu, 512, 480, 4, 2048);
    vm  = new RunningScreenViewModel(this.Dispatcher, this.m_ppuModel);

    setViewModel(vm);
    return ( true );
}

//----------------------------------------------------------------
/**   PPU を設定する。
**
**/
public  virtual  System.Boolean
setupPpuModel(PpuManagerModel ppuModel)
{
    RunningScreenViewModel  vm;

    this.m_ppuModel = ppuModel;
    vm  = new RunningScreenViewModel(this.Dispatcher, ppuModel);

    setViewModel(vm);
    return ( true );
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
/**   SourceBitmap プロパティ
**
**/
[Browsable(true)
  , Description("描画領域")
  , Category("表示")
]
public  System.Windows.Media.Imaging.WriteableBitmap?
SourceBitmap
{
    get { return  this.m_viewModel?.SourceBitmap; }
}


//========================================================================
//
//    Member Variables.
//

/**   ビューモデルクラス。  **/
private   GameScreenViewModel?      m_viewModel;

/**   PPU マネージャ。      **/
private   NesPpuManager?            m_wManPpu;

private   PpuManagerModel?          m_ppuModel;

/**   イメージレンダラ。    **/
private NesDbgWrap.Images.BitmapRenderer    m_bitmapRenderer
    = new NesDbgWrap.Images.BitmapRenderer();

/**   イメージ用バッファ。  **/
System.Drawing.Bitmap                       m_imgBuffer;

private NesDbgWrap.Images.FullColorImage?   m_screenImage;

private System.Drawing.Color    m_marginColor;

}   //  End class  GameScreen

}   //  End of namespace  NesWpfControl.Views
