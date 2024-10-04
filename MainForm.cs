using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snowfall
{
    public partial class MainForm : Form
    {

        private List<Snowflake> snowflakes = new List<Snowflake>();
        private Random random = new Random();
        private Timer timer = new Timer();
        private readonly Bitmap snowflakeImage = Properties.Resources.Snowflake;

        public MainForm()
        {
            InitializeComponent();

            // Настройка таймера
            timer.Interval = 30; // Чем меньше значение, тем плавнее анимация
            timer.Tick += Timer_Tick;
            timer.Start();

            // Генерация снежинок
            for (var i = 0; i < 20; i++) // Количество снежинок
            {
                snowflakes.Add(GenerateRandomSnowflake());
            }

            // Настройка двойной буферизации для предотвращения мерцания
            DoubleBuffered = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var snowflake in snowflakes)
            {
                snowflake.Y += snowflake.Speed; // Снежинка падает

                // Если снежинка достигла низа формы, она появляется снова сверху
                if (snowflake.Y > Height)
                {
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = random.Next(Width);
                }
            }

            // Обновление формы для перерисовки
            Invalidate();
        }

        private Snowflake GenerateRandomSnowflake()
        {
            var size = random.Next(15, 50); // Случайный размер снежинки
            var x = random.Next(Width); // Случайная позиция по X
            var y = random.Next(Height); // Случайная позиция по X
            var speed = random.Next(2, 10); // Случайная скорость падения

            return new Snowflake(x, y, size, speed);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            foreach (var snowflake in snowflakes)
            {
                // Отрисовка снежинки с масштабированием
                g.DrawImage(snowflakeImage, snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size);
            }
        }
    }
}
