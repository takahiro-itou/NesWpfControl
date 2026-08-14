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
    this.m_imgBuffer = new FullColorImage();
    this.m_trgModel  = new PpuManagerModel(
            base.Width, base.Height, base.BytesPerbPixel, base.LineStride);
}

//========================================================================
//
//    Member Variables.
//

private   readonly  PpuManagerModel         m_trgModel;

private   readonly  FullColorImage          m_imgBuffer;


}   //  End class  GameScreenViewModel

}   //  End of namespace  NesWpfControl.ViewModels
