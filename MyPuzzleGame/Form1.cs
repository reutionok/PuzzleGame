using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace MyPuzzleGame
{
    public partial  class Form1 : Form
    {
        public Form1()
        {            
            InitializeComponent();
            radioButton4x4.Checked = true;
        }

        
        OpenFileDialog openFileDialog = null;
        PictureBox picBoxWhole = null;
        Bitmap image;
        Bitmap previewImage;
        Image[] images = null;
        List<Bitmap>shufImages = null;
        MyPictureBox[] picBoxes = null;
        int numPieces = 0;
        int pieces = 4;
        bool isPlaying = false;
        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
                       if (openFileDialog == null)
                openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imagePath.Text = openFileDialog.FileName;
                previewImage = CreateBitmapImage(Image.FromFile(openFileDialog.FileName), previewPB.Width, previewPB.Height);
                image = CreateBitmapImage(Image.FromFile(openFileDialog.FileName), puzzleBoard.Width, puzzleBoard.Height);
                if (picBoxWhole == null)
                {
                    picBoxWhole = new PictureBox();
                    picBoxWhole.Height = previewPB.Height;
                    picBoxWhole.Width = previewPB.Width;
                    previewPB.Controls.Add(picBoxWhole);
                    btnCut.Enabled = true;

                }

                picBoxWhole.Image = previewImage;
            }
        }

        private Bitmap CreateBitmapImage(Image image, int width, int height)
        {
            Bitmap objBmpImage = new Bitmap(width, height);
            Graphics objGraphics = Graphics.FromImage(objBmpImage);
            objGraphics.Clear(System.Drawing.Color.White);

            objGraphics.DrawImage(image,
                new Rectangle(0, 0, width, height));
            objGraphics.Flush();

            return objBmpImage;
        }

        private void CreateBitmapImage(Image image, Image[] images, int index, int numRow, int numCol, int unitX, int unitY)
        {
            if (image != null)
            {
                images[index] = new Bitmap(unitX, unitY);
                Graphics objGraphics = Graphics.FromImage(images[index]);
                objGraphics.Clear(System.Drawing.Color.White);

                objGraphics.DrawImage(image,
                    new Rectangle(0, 0, unitX, unitY),
                    new Rectangle(unitX * (index % numRow), unitY * (index / numCol), unitX, unitY),
                    GraphicsUnit.Pixel);
                objGraphics.Flush();
            }
            
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
               
                PuzzleCutter(pieces, pieces);

                picBoxes = new MyPictureBox[numPieces];
                int[] indice = new int[numPieces];
                for (int i = 0; i < numPieces; i++)
                {
                    indice[i] = i;
                    if (picBoxes[i] == null)
                    {
                        picBoxes[i] = new MyPictureBox();
                        picBoxes[i].MouseClick += new MouseEventHandler(OnPuzzleClick);
                        picBoxes[i].BorderStyle = BorderStyle.Fixed3D;
                    }
                    picBoxes[i].Width = puzzleBoard.Width / pieces;
                    picBoxes[i].Height = puzzleBoard.Height / pieces;

                    picBoxes[i].Index = i;
                    picBoxes[i].Location = new System.Drawing.Point(picBoxes[i].Width * (i % pieces), picBoxes[i].Height * (i / pieces));
                    if (!puzzleBoard.Controls.Contains(picBoxes[i]))
                        puzzleBoard.Controls.Add(picBoxes[i]);
                }
                Shuffle(ref indice);
                shufImages = new List<Bitmap>(numPieces);
                for (int i = 0; i < numPieces; i++)
                {
                    shufImages.Add((Bitmap)images[indice[i]]);
                    picBoxes[i].Image = images[indice[i]];
                    picBoxes[i].ImageIndex = indice[i];
                }
                btnSolution.Enabled = true;
                isPlaying = true;
            }
        }
        private Image[] PuzzleCutter(int numRow, int numCol)
        {

            numPieces = numRow * numCol;
            int unitX = puzzleBoard.Width / numRow;
            int unitY = puzzleBoard.Height / numCol;
            images = new Image[numPieces];
           for (int i = 0; i < numPieces; i++)                      
                CreateBitmapImage(image, images, i, numRow, numCol, unitX, unitY);
                                 
            return images;
                    
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
            box2.Image = images[box1.ImageIndex];
            box2.ImageIndex = box1.ImageIndex;
            box1.Image = images[tmp];
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
                if (picBoxes[i].ImageIndex != picBoxes[i].Index)                                    
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
            Bitmap[,] bestChoise = GetBestPuzzleImage(shufImages);
            List<Bitmap> PB = new List<Bitmap>(numPieces);

            //for (int i = 0; i <bestChoise.GetLength(0); i++)
            //{
            //    for (int j = 0; j < bestChoise.GetLength(1);j++)
            //    {
            //        PB.Add(bestChoise[i, j]);
            //    }
            //}
            //for (int n = 0; n < numPieces; n++)
            //{
            //    picBoxes[n].Image = PB[n];
            //    picBoxes[n].ImageIndex = n;
            //}

            for (int i = 0; i < pieces * pieces; i++)
            {
                picBoxes[i].Image = images[i];
                picBoxes[i].ImageIndex = i;
            }
            isPlaying = false;
            System.Windows.MessageBox.Show("Success will be on your side next time", "Try again!");           
           
        }


        public Bitmap[,] GetBestPuzzleImage(List<Bitmap> list)
        {
          List<int> numbers = new List<int>(GetNumbers(list));

            Bitmap[,] bestChoice = null;

            double min = Int32.MaxValue;  //значення найменшої різниці малюнку(для ивзанчення найкращого результату)

            for (int i = 0; i < numbers.Count; i++)
            {
                int row = numbers[i];     //кількість рядків
                int col = list.Count / row;         //кількість стовпців

                
                Bitmap[,] possibleChoice = new Bitmap[row, col];

                double value = GetBestCurrentVariant(list, row, col, ref possibleChoice);

                if (min > value)
                {
                    bestChoice = (Bitmap[,])possibleChoice.Clone();

                    min = value;
                }
            }

            return bestChoice;
        }

        private List<int> GetNumbers(List<Bitmap> list)
        {
            List<int> numbers = new List<int>();

            for (int i = 1; i <= list.Count; i++)
            {
                if (list.Count % i == 0)
                {
                    numbers.Add(i);
                }
            }

            return numbers;
        }

        private double GetBestCurrentVariant(List<Bitmap> list, int row, int col, ref Bitmap[,] bestChoice)
        {
            Bitmap bestPiece;

            double min = Int32.MaxValue;            

            for (int j = 0; j < list.Count; j++)    //перебір всіх елементів колекції для даної розстановки
            {
                double total = 0; //показує повну різницю всього малюнку

                Bitmap[,] elements = new Bitmap[row, col];  //отриманий результат

                List<Bitmap> cash = new List<Bitmap>(list);     //колекція доступних елементів

                elements[0, 0] = cash[j];

                cash.RemoveAt(j);

                for (int irow = 0; irow < row - 1; irow++)    // перебір по рядкам
                {
                    for (int icol = 0; icol < col - 1; icol++)  //перебір по стовпцям
                    {
                        //for(int icas = 0; icas<cash.Count;icas++)   //перебір по доступним елементам
                        //{
                        //}

                        bestPiece = GetRightImage(elements[irow, icol], cash, ref total);//отримуємо найкращий правий фрагмент
                        elements[irow, icol + 1] = bestPiece;//додаємо отриманий фрагмент до результату

                        cash.Remove(bestPiece);//видаляємо недоступний елемент

                    }

                    bestPiece = GetBottomImage(elements[irow, 0], cash, ref total);//отримуємо найкращий нижній фрагмент

                    elements[irow + 1, 0] = bestPiece;//додаємо отриманий фрагмент до результату

                    cash.Remove(bestPiece);//видаляємо недоступний елемент
                }

                //додаємо фрагменти до останнього рядка
                for (int icol = 0; icol < col - 1; icol++)  //перебір по стовпцям
                {
                    bestPiece = GetRightImage(elements[row - 1, icol], cash, ref total);//отримуємо найкращий правий фрагмент
                    elements[row - 1, icol + 1] = bestPiece;//додаємо отриманий фрагмент до результату

                    cash.Remove(bestPiece);//видаляємо недоступний елемент
                }

                if (min > total)
                {
                    bestChoice = (Bitmap[,])elements.Clone();
                    min = total;
                }
            }

            return min;
        }

        private Bitmap GetRightImage(Bitmap first, List<Bitmap> list, ref double totalDifference)
        {
            double min = Int32.MaxValue;

            Color[] left = new Color[ first.Height];            
            
                for (int j = 0; j < first.Height; j++)
                    left[j] = first.GetPixel(j, first.Height - 1);
            
            Bitmap next = null;
            for (int n = 0; n < list.Count; n++)
            {
                Color[] right = new Color[first.Height];
                for (int j = 0; j < first.Height; j++)
                        right[j] = list[n].GetPixel(j,0);
                

                double value = GetRightDifference(left, right);

                if (min > value)
                {
                    next = list[n];
                    min = value;
                }
            }
            totalDifference += min;

            return next;
        }
        private double GetRightDifference(Color[] left, Color[] right)
        {
            double rightDifference = 0;

            try
            {
                if (left.Length != right.Length)
                    throw new Exception("Щось пішло не так");

                for (int i = 0; i < left.Length; i++)
                {
                    rightDifference += GetDifference(left[i], right[i]);
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            return rightDifference;

        }

        private double GetDifference(Color first, Color second)
        {
            int dr = Math.Abs(first.R - second.R);
            int dg = Math.Abs(first.G - second.G);
            int db = Math.Abs(first.B - second.B);
            int da = Math.Abs(first.A - second.A);

            return Math.Sqrt(dr * dr + dg * dg + db * db);
        }



        private Bitmap GetBottomImage(Bitmap first, List<Bitmap> list, ref double totalDifference)
        {
            double min = Int32.MaxValue;
            Color[] up = new Color[first.Width];
            
            for (int i = 0; i < first.Height; i++)           
                 up[i] = first.GetPixel(first.Width-1,i);
            

            Bitmap next = null;
            for (int n = 0; n < list.Count; n++)
            {
                Color[] down = new Color[first.Width];
                for (int i = 0; i < first.Width; i++)
                    down[i] = list[n].GetPixel(0, i);
                
               
                double value = GetBottomDifference(up, down);

                if (min > value)
                {
                    next = list[n];
                    min = value;
                }
            }

            totalDifference += min;

            return next;
        }

        private double GetBottomDifference(Color[] up, Color[] down)
        {
            double bottomDifference = 0;

            try
            {
                if (up.Length != down.Length)
                    throw new Exception("Щось пішло не так і тут");

                for (int i = 0; i < up.Length; i++)
                {
                    bottomDifference += GetDifference(up[i], down[i]);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            return bottomDifference;
        }
    }
}














//---------------------------------------
//    Bitmap[] img = new Bitmap[PB.Length];
//    Color[,] cl = new Color[img.Length, 35];
//    for (int i = 0; i < img.Length; i++)
//    {
//        img[i] = (Bitmap)PB[i].Image;

//        for (int h = 0; h < img[i].Height; h++)
//        {
//            cl[i,h]= img[i].GetPixel(img[i].Width - 1, h);
//        }

//    }

//    for (int j = 0; j < img.Length; j++)
//    {
//        for (int i = 0; i < img.Length; i++)
//        {


//        }
//    }

//    var p =image.GetPixel(image.Width-1, image.Height-1);
//    for (int i = 0; i < pieces * pieces; i++)
//    {
//        PB[i].Image = imgarray[i];
//        PB[i].ImageIndex = i;

//    }
//    isPlaying = false;
//    System.Windows.MessageBox.Show("Success will be on your side next time", "Try again!");
//}


//// closed match for hues only:
//int closestColor1(List<Color> colors, Color target)
//{
//    var hue1 = target.GetHue();
//    var diffs = colors.Select(n => getHueDistance(n.GetHue(), hue1));
//    var diffMin = diffs.Min(n => n);
//    return diffs.ToList().FindIndex(n => n == diffMin);
//}

//// closed match in RGB space
//int closestColor2(List<Color> colors, Color target)
//{
//    var colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
//    return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs);
//}

//// weighed distance using hue, saturation and brightness
//int closestColor3(List<Color> colors, Color target)
//{
//    float hue1 = target.GetHue();
//    var num1 = ColorNum(target);
//    var diffs = colors.Select(n => Math.Abs(ColorNum(n) - num1) +
//                                   getHueDistance(n.GetHue(), hue1));
//    var diffMin = diffs.Min(x => x);
//    return diffs.ToList().FindIndex(n => n == diffMin);
//}

//// color brightness as perceived:
//float getBrightness(Color c)
//{ return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f; }

// distance between two hues:
//float getHueDistance(float hue1, float hue2)
//        {
//            float d = Math.Abs(hue1 - hue2); return d > 180 ? 360 - d : d;
//        }

        ////  weighed only by saturation and brightness (from my trackbars)
        //float ColorNum(Color c)
        //{
        //    return c.GetSaturation() * factorSat +
        //                getBrightness(c) * factorBri;
        //}

        //// distance in RGB space
        //int ColorDiff(Color c1, Color c2)
        //{
        //    return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
        //                           + (c1.G - c2.G) * (c1.G - c2.G)
        //                           + (c1.B - c2.B) * (c1.B - c2.B));
        //}


