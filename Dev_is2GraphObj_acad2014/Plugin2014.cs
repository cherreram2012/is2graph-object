using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Windows;

namespace Dev_is2GraphObj
{
  //=============================================================================================//
  // ClassName  :
  //
  // Description:
  //
  // Revision   :
  //=============================================================================================//
  public class Plugin : Autodesk.AutoCAD.Runtime.IExtensionApplication
  {
    //------------------------------------------------------------------------//
    //
    public void Initialize()
    {
      Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
      ed.WriteMessage("Dev_is2GraphObject Library.dll - loading succesfully.");

      if (ComponentManager.Ribbon == null)
      {
        //load the custom Ribbon on startup, but at this point
        //the Ribbon control is not available, so register for
        //an event and wait.
        ComponentManager.ItemInitialized += ComponentManager_ItemInitialized;
            
      }
    }

    //------------------------------------------------------------------------//
    //
    public void Terminate()
    {
      
    }

    //------------------------------------------------------------------------//
    //
    private void ComponentManager_ItemInitialized(object sender, RibbonItemEventArgs e)
    {
      //now one Ribbon item is initialized, but the Ribbon control
      //may not be available yet, so check if before
      if (ComponentManager.Ribbon != null)
      {
        //CardioideTabRibbon();

        //and remove the event handler
        ComponentManager.ItemInitialized -=
          new EventHandler<RibbonItemEventArgs>(ComponentManager_ItemInitialized);
      }
    }

    //------------------------------------------------------------------------//
    //
    public void CardioideTabRibbon()
    {
      RibbonControl ribbonControl = Autodesk.Windows.ComponentManager.Ribbon;
      RibbonTab TabCardioide = new RibbonTab();
      TabCardioide.Title = "Cardioide";
      TabCardioide.Id = "RIBBON_TAB_ID_CARDIOIDE";

      ribbonControl.Tabs.Add(TabCardioide);

      // create Ribbon panels
      RibbonPanelSource panel1Panel = new RibbonPanelSource();
      panel1Panel.Title = "Cardioide - Plugin";
      RibbonPanel Panel1 = new RibbonPanel();
      Panel1.Source = panel1Panel;
      TabCardioide.Panels.Add(Panel1);

      RibbonButton button1 = new RibbonButton();
      button1.Text = "  Variante 1ra  ";
      button1.ShowText = true;
      button1.ShowImage = true;
      button1.IsEnabled = false;
      //button1.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button1.Orientation = System.Windows.Controls.Orientation.Vertical;
      button1.Size = RibbonItemSize.Large;
      //button1.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button1);

      RibbonButton button2 = new RibbonButton();
      button2.Text = "  Variante 2da  ";
      button2.ShowText = true;
      button2.ShowImage = true;
      button2.IsEnabled = false;
      //button1.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button1.Orientation = System.Windows.Controls.Orientation.Vertical;
      button2.Size = RibbonItemSize.Large;
      //button2.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button2);

      RibbonButton button3 = new RibbonButton();
      button3.Text = "  Variante 3ra  ";
      button3.ShowText = true;
      button3.ShowImage = true;
      button3.IsEnabled = false;
      //button1.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button1.Orientation = System.Windows.Controls.Orientation.Vertical;
      button3.Size = RibbonItemSize.Large;
      //button3.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button3);

      RibbonButton button4 = new RibbonButton();
      button4.Text = "  Variante 4ta  ";
      button4.ShowText = true;
      button4.ShowImage = true;
      button4.IsEnabled = false;
      //button4.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button4.Orientation = System.Windows.Controls.Orientation.Vertical;
      button4.Size = RibbonItemSize.Large;
      //button4.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button4);

      RibbonButton button5 = new RibbonButton();
      button5.Text = "  Variante 5ta  ";
      button5.ShowText = true;
      button5.ShowImage = true;
      button5.IsEnabled = false;
      //button5.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button5.Orientation = System.Windows.Controls.Orientation.Vertical;
      button5.Size = RibbonItemSize.Large;
      //button5.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button5);

      RibbonButton button6 = new RibbonButton();
      button6.Text = "  Variante 6ta  ";
      button6.ShowText = true;
      button6.ShowImage = true;
      //button6.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button6.Orientation = System.Windows.Controls.Orientation.Vertical;
      button6.Size = RibbonItemSize.Large;
      //button6.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button6);

      RibbonButton button7 = new RibbonButton();
      button7.Text = "  Variante 7ma  ";
      button7.ShowText = true;
      button7.ShowImage = true;
      //button7.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button7.Orientation = System.Windows.Controls.Orientation.Vertical;
      button7.Size = RibbonItemSize.Large;
      button7.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button7);

      RibbonButton button8 = new RibbonButton();
      button8.Text = "  Variante 8va  ";
      button8.ShowText = true;
      button8.ShowImage = true;
      //button8.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button8.Orientation = System.Windows.Controls.Orientation.Vertical;
      button8.Size = RibbonItemSize.Large;
      //button8.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button8);

      RibbonButton button9 = new RibbonButton();
      button9.Text = "  Variante 9na  ";
      button9.ShowText = true;
      button9.ShowImage = true;
      //button9.LargeImage = Images.getBitmap(Properties.Resources.dtm);
      //button9.Orientation = System.Windows.Controls.Orientation.Vertical;
      button9.Size = RibbonItemSize.Large;
      //button9.CommandHandler = new Variante7CmdHandler();
      panel1Panel.Items.Add(button9);
    }
  }

  public class Images
  {
    /*public static BitmapImage getBitmap(System.Drawing.Bitmap image)
    {
      MemoryStream stream = new MemoryStream();
      BitmapImage bmp = new BitmapImage();

      image.Save(stream, ImageFormat.Png);
      bmp.BeginInit();
      bmp.StreamSource = stream;
      bmp.EndInit();

      return bmp;
    }*/
  }

  //=============================================================================================//
  // ClassName  :
  //
  // Description:
  //
  // Revision   :
  //=============================================================================================//
  public class Variante7CmdHandler : System.Windows.Input.ICommand
  {
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      if (parameter is RibbonButton)
      {
        Document acDoc = Application.DocumentManager.MdiActiveDocument;
        acDoc.SendStringToExecute("Cardioide7 ", true, false, false);
      }
    }
  }
}
