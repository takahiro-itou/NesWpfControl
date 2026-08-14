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
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using WpfControl.Common;

using NesWpfControl.Models;

using FullColorImage = NesDbgWrap.Images.FullColorImage;


namespace  NesWpfControl.ViewModels  {

public  class  GameScreenViewModel : INotifyPropertyChanged
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
GameScreenViewModel(
        Dispatcher      dispatcher,
        PpuManagerModel model)
{
    const  int  nWidth  = 256;
    const  int  nHeight = 240;
    int         cbPixel = 4;
    int         lStride = 0;

    System.IntPtr       ptrBuf;
    WriteableBitmap     bmpCanvas;

    this.m_dispatcher = dispatcher;

    bmpCanvas = new WriteableBitmap(
            nWidth, nHeight, 96, 96,
            PixelFormats.Pbgra32, null);
    this.m_mainImage = new FullColorImage();
    this.m_imgBuffer = new FullColorImage();
    this.m_imgWidth  = nWidth;
    this.m_imgHeight = nHeight;
    this.m_trgModel = new PpuManagerModel(nWidth, nHeight, cbPixel, lStride);

    bmpCanvas.Lock();
    cbPixel = (bmpCanvas.Format.BitsPerPixel + 7) >> 3;
    lStride = bmpCanvas.BackBufferStride;

    ptrBuf  = bmpCanvas.BackBuffer;
    this.m_mainImage.createImage(nWidth, nHeight, cbPixel, lStride, ptrBuf);
    bmpCanvas.Unlock();

    this.m_imgBuffer.allocateImage(nWidth, nHeight, cbPixel, lStride);
    this.m_bmpCanvas = bmpCanvas;

    this.m_drawImageCommand  = new SimpleCommand<int>(
        parameter => this.drawImageTaskAsync(parameter),
        _ => this.canRunTask()
    );
    this.m_clearImageCommand = new SimpleCommand<int>(
        parameter => this.m_trgModel.clearImage(parameter)
    );

    this.m_progress  = new System.Progress<int>(updateProgress);
    this.m_isRunning = false;
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   タスクを実行可能か判定する。
**
**/
public  virtual  bool
canRunTask()
{
    return ( ! this.IsRunning );
}

//----------------------------------------------------------------
/**   モデルのタスクを非同期で実行する。
**
**/
public  virtual  async  void
drawImageTaskAsync(int parameter)
{
    this.IsRunning  = true;

    Task<int>  task = Task.Run<int>(
        () => this.executeCommand(this.m_progress, parameter)
    );
    int  result = await task;

    this.IsRunning  = false;
}


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
/**   タスクを実行するコマンドを取得するプロパティ。
**
**/
public  virtual  ICommand
ClearImageCommand {
    get { return  this.m_clearImageCommand; }
}

//----------------------------------------------------------------
/**   タスクを実行するコマンドを取得するプロパティ。
**
**/
public  virtual  ICommand
DrawImageCommand {
    get { return  this.m_drawImageCommand; }
}

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
/**   モデルのタスクを実行する。
**
**/

protected  virtual  int
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

//----------------------------------------------------------------
/**
**
**/
protected  virtual  int
raiseCanExecuteChanged()
{
    this.m_dispatcher.Invoke(
        () => {
        }
    );

    return ( 0 );
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

//----------------------------------------------------------------
/**
**
**/
protected  virtual  void
updateProgress(int progressValue)
{
    this.m_bmpCanvas.Lock();
    this.m_mainImage.copyImage(this.m_trgModel.ImageBuffer);
    this.m_bmpCanvas.AddDirtyRect(
            new Int32Rect(0, 0, this.m_imgWidth, this.m_imgHeight)
    );
    this.m_bmpCanvas.Unlock();
}


//========================================================================
//
//    Member Variables.
//

private  readonly   Dispatcher              m_dispatcher;
private  readonly   FullColorImage          m_mainImage;

private  readonly   PpuManagerModel         m_trgModel;

private  readonly   FullColorImage          m_imgBuffer;
private  readonly   int                     m_imgWidth;
private  readonly   int                     m_imgHeight;

private  readonly   System.IProgress<int>   m_progress;

private  readonly   SimpleCommand<int>      m_drawImageCommand;
private  readonly   SimpleCommand<int>      m_clearImageCommand;

private  WriteableBitmap    m_bmpCanvas;

private  bool               m_isRunning;


}   //  End class  GameScreenViewModel

}   //  End of namespace  NesWpfControl.ViewModels
