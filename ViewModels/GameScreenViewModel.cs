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


using FullColorImage = SampleWrapper.Images.FullColorImage;


namespace  NesWpfControl.ViewModels  {

public  class  GameScreenViewModel
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public  GameScreenViewModel()
{
}


//========================================================================
//
//    Public Member Functions.
//


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/
public  event PropertyChangedEventHandler?  PropertyChanged;


//----------------------------------------------------------------
/**
**
**/
public  bool
IsRunning  {
    get { return  this.m_isRunning; }
    private set {
        this.m_isRunning = value;
        raisePropertyChanged();
        raiseCanExecuteChanged();
    }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  WriteableBitmap
SourceBitmap {
    get { return  this.m_bmpCanvas; }
}


//========================================================================
//
//    Protected Member Functions.
//

//----------------------------------------------------------------
/**
**
**/
protected  virtual  void
raiseCanExecuteChanged()
{
}

//----------------------------------------------------------------
/**
**
**/
protected  virtual  void
raisePropertyChanged(
        [CallerMemberName]  System.String?  propertyName = null)
{
    PropertyChanged?.Invoke(
            this, new PropertyChangedEventArgs(propertyName));
}


//========================================================================
//
//    Member Variables.
//

private  readonly   FullColorImage              m_mainImage;

private  readonly   FullColorImage              m_imgBuffer;
private  readonly   int                         m_imgWidth;
private  readonly   int                         m_imgHeight;

private  readonly   System.IProgress<int>       m_progress;

private  WriteableBitmap    m_bmpCanvas;

private  bool               m_isRunning;


}   //  End class  GameScreenViewModel

}   //  End of namespace  NesWpfControl.ViewModels
