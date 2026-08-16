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
    int  colBG  = unchecked((int)0x80000000);
    this.m_ppuModel?.clearScreenImage(colBG);
}

//----------------------------------------------------------------
/**   デフォルトの描画処理を行う。
**
**/
public  virtual  void
drawScreen()
{
    this.m_ppuModel?.drawScreenImage();
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

    this.m_ppuModel = new PpuManagerModel(manPpu, 512, 480, 4, 2048);
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
/**   ゲーム画面を更新する。
**
**/
public  virtual  void
updateScreen()
{
    this.m_ppuModel?.updateBitmapChanged();
}


//========================================================================
//
//    Properties.
//

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
private   PpuManagerModel?          m_ppuModel;


}   //  End class  GameScreen

}   //  End of namespace  NesWpfControl.Views
