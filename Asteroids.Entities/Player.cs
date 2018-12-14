using System.Drawing;
using System.Windows.Forms;
using Pen = System.Drawing.Pen;

namespace Asteroids.Entities
{
    public class Player
    {
        public PointF Position { get; set; }
        
        public Player()
        {
            Position = new PointF(100,100);
        }

        // TODO Separete model and View
        public void Draw(ref PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            Pen pen = new Pen(System.Drawing.Color.AliceBlue, 1.5f);
            
            G.DrawLine(pen, Position.X, Position.Y, Position.X, Position.Y-10f);
            G.DrawLine(pen, Position.X, Position.Y, Position.X-10f, Position.Y);
            
            G.DrawLine(pen, Position.X, Position.Y-10f, Position.X+10f, Position.Y+10f);
            G.DrawLine(pen, Position.X-10f, Position.Y, Position.X+10f, Position.Y+10f);

            pen.Dispose();
        }

        public void Move()
        {
            
        }

        public void Update(float dt)
        {
            
        }
    }
}