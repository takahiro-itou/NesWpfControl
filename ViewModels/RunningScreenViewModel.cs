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
    this.m_imgBuffer = new FullColorImage();
    this.m_imgBuffer.allocateImage(
            base.Width, base.Height, base.BytesPerPixel, base.LineStride);

    this.m_trgModel  = new PpuManagerModel(
            base.Width, base.Height, base.BytesPerPixel, base.LineStride);
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**   イメージバッファを取得するプロパティ。
**
**/
public  virtual  FullColorImage?
ImageBuffer  {
    get { return  this.m_imgBuffer; }
}


//========================================================================
//
//    Protected Member Functions.
//

//----------------------------------------------------------------
/**   モデルのタスクを実行する。
**
**/

protected  override  int
executeCommand(
        System.IProgress<int>   progress,
        int                     parameter)
{
    int  interval = 2000 / parameter;
    int  count    = parameter;

    for ( int i = 1; i <= count; ++ i ) {
        this.m_trgModel.drawSampleImage();
        progress.Report(i);
        System.Threading.Thread.Sleep(interval);
    }

    return ( 0 );
}


//========================================================================
//
//    Member Variables.
//

private   readonly  PpuManagerModel         m_trgModel;

private   readonly  FullColorImage          m_imgBuffer;


}   //  End class  GameScreenViewModel

}   //  End of namespace  NesWpfControl.ViewModels
