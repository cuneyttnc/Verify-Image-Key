using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

class VerifyImageKey
{
    #region Private Members
    private int m_iCharWidth = 20;
    private int m_iImageWidth = 0;
    private int m_iImageHeight = 0;
    private byte m_iChaLength;
    private string m_sVerfyKey = string.Empty;
    private Brush oFontColor = new SolidBrush(Color.DarkBlue);
    private Brush oBackColor = new SolidBrush(Color.White);
    #endregion

    #region Properties
    public string VerifyKey
    {
        get
        {
        if (this.m_sVerfyKey.Equals(string.Empty))
            throw new Exception("Resim Oluşturulamadı !");
        return this.m_sVerfyKey;
        }
    }
      
    public int CharWidth
    {
        get
        {
        return this.m_iCharWidth;
        }
        set
        {
        if (value < 10)
            throw new Exception("Uzunluk en fazla 10 olabilir!");
        this.m_iCharWidth = value;
        }
    }
    public byte CharacterLength
    {
        get
        {
        return this.m_iChaLength;
        }
        set
        {
        if (!(value > 0))
            throw new Exception("Karakter sayısı girin!");
        this.m_iChaLength = value;
        }
    }

    public Brush FontColor
    {
        get
        {
        return this.oFontColor;
        }
        set
        {
        this.oFontColor = value;
        }
    }
    public Brush BackColor
    {
        get
        {
        return this.oBackColor;
        }
        set
        {
        this.oBackColor = value;
        }
    }
    #endregion

    #region Constructures
    public VerifyImageKey(byte iCharLength)
    {
        this.m_iChaLength = iCharLength;
        this.m_iImageWidth = this.m_iCharWidth * this.m_iChaLength + 20;   // Set ImageWidth value
        this.m_iImageHeight = this.m_iCharWidth * 2;   // Set ImageHeight value
    }

    public VerifyImageKey(byte iCharLength, int iCharWidth)
    {
        this.m_iChaLength = iCharLength;
        this.CharWidth = iCharWidth;
        this.m_iImageWidth = this.m_iCharWidth * this.m_iChaLength + 20;   // Set ImageWidth value
        this.m_iImageHeight = this.m_iCharWidth * 2;   // Set ImageHeight value
    }
    #endregion

    #region Methods
    public Bitmap GenerateImage()
    {
        Bitmap oBitmap = new Bitmap(this.m_iImageWidth, this.m_iImageHeight);
        Graphics oGraphics = Graphics.FromImage(oBitmap);
        oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        Rectangle oRectangle = new Rectangle(0, 0, this.m_iImageWidth, this.m_iImageHeight);
        oGraphics.FillRectangle(oBackColor, oRectangle);
        m_sVerfyKey = GenerateStringKey();
        oGraphics.DrawString(this.m_sVerfyKey, new Font("Verdana", this.m_iCharWidth, FontStyle.Regular, GraphicsUnit.Point), oFontColor, oRectangle);

        oBitmap = FillDirty(oBitmap);
        oGraphics.Dispose();

        return oBitmap;
    }

    private Bitmap FillDirty(Bitmap oBitmap)
    {
        Graphics oGraphics = Graphics.FromImage(oBitmap);
        Pen oPen;      
        Random oRand = new Random();
        for (int i = 0; i < this.m_iChaLength * 10; i++)
        {
        oPen = new Pen(Color.FromArgb(oRand.Next(0, 255), oRand.Next(0, 255), oRand.Next(0, 255)));
        oGraphics.DrawBezier(oPen, new Point(oRand.Next(0, m_iImageWidth), oRand.Next(0, m_iImageWidth)),
                                new Point(oRand.Next(0, m_iImageWidth), oRand.Next(0, m_iImageWidth)),
                                new Point(oRand.Next(0, m_iImageWidth), oRand.Next(0, m_iImageWidth)),
                                new Point(oRand.Next(0, m_iImageWidth), oRand.Next(0, m_iImageWidth)));
        }
        oGraphics.Dispose();
        return oBitmap;
    }

    private string GenerateStringKey()
    {
        string sKey = string.Empty;
        Random oRand = new Random();
        int iChar;

        for (int i = 0; i < this.m_iChaLength; i++)
        {
        iChar = oRand.Next(48, 91);
        if (iChar > 57 && iChar < 65)
        {
            i--;
            continue;
        }
        sKey  += (char)iChar;
        }
        return sKey;
    }
    #endregion

}