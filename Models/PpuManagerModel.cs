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

using FullColorImage = NesDbgWrap.Images.FullColorImage;
using NesPpuManager  = NesDbgWrap.NesMan.BasePpuCore;


namespace  NesWpfControl.Models  {

public  class  PpuManagerModel
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
PpuManagerModel(
        NesPpuManager   manPpu,
        int             nWidth,
        int             nHeight,
        int             cbPixel,
        int             lStride)
{
    this.m_wManPpu  = manPpu;
    this.m_ibWidth  = nWidth;
    this.m_ibHeight = nHeight;

    this.m_imgBack  = new FullColorImage();
    this.m_imgBack.allocateImage(nWidth, nHeight, cbPixel, lStride);

    this.m_wManPpu.TargetImage  = this.m_imgBack;
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   画像をクリアする。
**
**/
public  virtual  void
clearScreenImage(int colBG)
{
    this.m_imgBack.fillRectangle(
            0, 0, this.m_ibWidth, this.m_ibHeight, colBG);
    notifyBitmapChanged();
}

//----------------------------------------------------------------
/**   画面イメージを描画する。
**
**/
public  virtual  void
drawScreenImage()
{
    this.m_wManPpu?.drawScreen();
    notifyBitmapChanged();
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**   イメージバッファを取得するプロパティ。
**
**/

public  FullColorImage
ImageBuffer {
    get { return  this.m_imgBack; }
}


//========================================================================
//
//    Public Events.
//

public  event  Action?  BitmapChanged;

protected  void  notifyBitmapChanged()
{
    this.BitmapChanged?.Invoke();
}


//========================================================================
//
//    Member Variables.
//

private   readonly  NesPpuManager   m_wManPpu;

private   readonly  FullColorImage  m_imgBack;

private   readonly  int             m_ibWidth;
private   readonly  int             m_ibHeight;


}   //  End class  PpuManagerModel

}   //  End of namespace  NesWpfControl.Views
