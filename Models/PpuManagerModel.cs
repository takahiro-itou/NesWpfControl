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
    this.m_manPpu   = manPpu;
    this.m_imgBack  = new FullColorImage();
    this.m_ibWidth  = nWidth;
    this.m_ibHeight = nHeight;

    this.m_imgBack.allocateImage(nWidth, nHeight, cbPixel, lStride);
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
clearImage(int colBG)
{
    this.m_imgBack.fillRectangle(
            0, 0, this.m_imgWidth, this.m_imgHeight, colBG);
    notifyBitmapChanged();
}

//----------------------------------------------------------------
/**   サンプル画像を描画する。
**
**/
public  virtual  void
drawSampleImage()
{
    int     cAlpha;
    int     colBG, colTL, colTR, colBL, colBR;
    System.Random   rnd = new System.Random();

    //  色を適当に決める。背景はある程度明るい色
    cAlpha  = 255 << 24;
    colBG = rnd.Next(16777216) | cAlpha | 0x00808080;

    //  色を適当に決める。
    colTL = rnd.Next(256) | cAlpha | 0x00000080;
    colTR = (rnd.Next(256) <<  8) | cAlpha | 0x00008080;
    colBL = rnd.Next(256);
    colBL = (colBL | colBL <<  8) | cAlpha | 0x00008080;
    colBR = (rnd.Next(256) << 16) | cAlpha | 0x00800000;

    this.m_imgBack.drawSample(colBG, colTL, colTR, colBL, colBR);
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

private   readonly  NesPpuManager   m_manPpu;

private   readonly  FullColorImage  m_imgBack;

private   readonly  int             m_ibWidth;
private   readonly  int             m_ibHeight;


}   //  End class  PpuManagerModel

}   //  End of namespace  NesWpfControl.Views
