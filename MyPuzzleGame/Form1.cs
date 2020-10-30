using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace MyPuzzleGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {            
            InitializeComponent();
            radioButton4x4.Checked = true;
        }

        
        OpenFileDialog openFileDialog = null;
        PictureBox picBoxWhole = null;
        Image image;
        Image[] imgarray = null;
        MyPictureBox[] PB = null;
        int pieces = 4;
        bool isPlaying = false;
        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
                       if (openFileDialog == null)
                openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imagePath.Text = openFileDialog.FileName;
                image = CreateBitmapImage(Image.FromFile(openFileDialog.FileName));
                if (picBoxWhole == null)
                {
                    picBoxWhole = new PictureBox();
                    picBoxWhole.Height = previewPB.Height;
                    picBoxWhole.Width = previewPB.Width;
                    previewPB.Controls.Add(picBoxWhole);
                    btnCut.Enabled = true;
                   
                }
                   
                picBoxWhole.Image = image;
            }
        }

        private Bitmap CreateBitmapImage(Image image)
        {
            Bitmap objBmpImage = new Bitmap(previewPB.Width, previewPB.Height);
            Graphics objGraphics = Graphics.FromImage(objBmpImage);
            objGraphics.Clear(Color.White);

            objGraphics.DrawImage(image,
                new Rectangle(0, 0, previewPB.Width, previewPB.Height));
            objGraphics.Flush();

            return objBmpImage;
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                if (System.Windows.MessageBox.Show("A puzzle is in progress.  Start new puzzle?", "New Puzzle", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    isPlaying = false;
                }
            }

            if (!isPlaying)
            {
                if (radioButton4x4.Checked == true)
                {
                    pieces = 4;
                }
                else if (radioButton6x6.Checked == true)
                {
                    pieces = 6;
                }
                else
                {
                    pieces = 9;
                }
                puzzleBoard.Controls.Clear();
               
                PuzzleCutter(pieces);
                btnSolution.Enabled = true;
                isPlaying = true;
            }
        }
        
        private void PuzzleCutter(int parts)  
        {
            Control board = puzzleBoard;  
            int total = parts * parts;
            PB = new MyPictureBox[total];

            imgarray = new Image[total];
            int W = image.Width / parts;
            int H = image.Height / parts;
            int size = board.Height/parts;
            int[] indice = new int[parts * parts];
            for (int x = 0; x < parts; x++)
            {
                for (int y = 0; y < parts; y++)
                {
                    var index = x * parts + y;
                    imgarray[index] = new Bitmap(W, H);
                    using (Graphics graphics = Graphics.FromImage(imgarray[index]))
                    {
                        graphics.DrawImage(image, new Rectangle(0, 0, W, H),
                                           new Rectangle(x * W, y * H, W, H), GraphicsUnit.Pixel);
                    }
                    PB[index] = new MyPictureBox
                    {
                        Name = "P" + index,
                        Size = new Size(size, size),
                        Location = new Point(x * size, y * size),
                        Image = imgarray[index],
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Index = index
                    };
                    indice[index] = index;
                    PB[index].MouseClick += new MouseEventHandler(OnPuzzleClick);
                    PB[index].BorderStyle = BorderStyle.Fixed3D;                    
                    board.Controls.Add(PB[index]);
                }
            }
            Shuffle(ref indice);
            for (int i = 0; i < parts*parts; i++)
            {
                PB[i].Image = imgarray[indice[i]];
                PB[i].ImageIndex = indice[i];
            }
        }

        MyPictureBox firstBox = null;
        MyPictureBox secondBox = null;
        MyPictureBox currentBox = null;
        public void OnPuzzleClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        currentBox = (MyPictureBox)sender;
                        currentBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        currentBox.Refresh();
                        break;
                    }

                case MouseButtons.Left:
                    {
                        if (firstBox == null)
                        {
                            firstBox = (MyPictureBox)sender;
                            firstBox.BorderStyle = BorderStyle.FixedSingle;
                        }
                        else if (secondBox == null)
                        {
                            secondBox = (MyPictureBox)sender;
                            firstBox.BorderStyle = BorderStyle.Fixed3D;
                            secondBox.BorderStyle = BorderStyle.FixedSingle;
                            SwitchImages(firstBox, secondBox);
                            firstBox = null;
                            secondBox = null;
                        }
                        break;
                    }
            }
            
        }

        private void SwitchImages(MyPictureBox box1, MyPictureBox box2)
        {
            int tmp = box2.ImageIndex;
            box2.Image = imgarray[box1.ImageIndex];
            box2.ImageIndex = box1.ImageIndex;
            box1.Image = imgarray[tmp];
            box1.ImageIndex = tmp;
            if (isSeccessful())
            {
                System.Windows.MessageBox.Show("Well done! ", "Congratulations");
            }
        }

        private bool isSeccessful()
        {
            for (int i = 0; i < pieces*pieces; i++)
            {
                if (PB[i].ImageIndex != PB[i].Index)                                    
                    return false;
                
            }
            isPlaying = false;
            return true;
        }
        private void Shuffle(ref int[] array)
        {
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n);
                n--;
                int temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        private void btnSolution_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pieces * pieces; i++)
            {
                PB[i].Image = imgarray[i];
                PB[i].ImageIndex = i;

            }
            isPlaying = false;
            System.Windows.MessageBox.Show("Success will be on your side next time", "Try again!");
        }
    }
}
