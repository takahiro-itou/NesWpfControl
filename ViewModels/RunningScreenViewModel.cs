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

using System.Windows.Threading;

using NesWpfControl.Models;

using FullColorImage = NesDbgWrap.Images.FullColorImage;
using NesPpuManager  = NesDbgWrap.NesMan.BasePpuCore;


namespace  NesWpfControl.ViewModels  {

public  class  RunningScreenViewModel : GameScreenViewModel
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public
RunningScreenViewModel(
        Dispatcher      dispatcher,
        NesPpuManager   manPpu)
    : base(dispatcher)
{
    this.m_trgModel  = new PpuManagerModel(
            manPpu,  base.Width * 2, base.Height * 2,
            base.BytesPerPixel, base.LineStride * 2);
    this.m_trgModel.BitmapChanged += handleBitmapChangedEvent;
}

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public
RunningScreenViewModel(
        Dispatcher      dispatcher,
        PpuManagerModel ppuModel)
    : base(dispatcher)
{
    this.m_trgModel  = ppuModel;
    this.m_trgModel.BitmapChanged += handleBitmapChangedEvent;
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**   イメージバッファを取得するプロパティ。
**
**/
public  override  FullColorImage?
ImageBuffer  {
    get { return  this.m_trgModel.ImageBuffer; }
}


//========================================================================
//
//    Protected Member Functions.
//

//========================================================================
//
//    Member Variables.
//

private   readonly  PpuManagerModel         m_trgModel;


}   //  End class  GameScreenViewModel

}   //  End of namespace  NesWpfControl.ViewModels
