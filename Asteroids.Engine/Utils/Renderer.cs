using OpenTK;

namespace Asteroids.Engine.Utils
{
    public static class Renderer
    {

        public static Matrix4 ModelMatrix { get; set; }
        public static Matrix4 ViewMatrix { get; set; }
        public static Matrix4 ProjectionMatrix { get; set; }
        
        
        #region Draw tools

        /// <summary>
        ///     Draw triangle
        /// </summary>
        /// <param name="verteces">Array of vertces coordinate (x,y,z)</param>
        /// <param name="colors">Array of verteces color (r,g,b,a)</param>
        public static void DrawTriangle(float[] verteces, float[] colors)
        {
            
        }

        /// <summary>
        ///     Draw line
        /// </summary>
        /// <param name="verteces">Array of vertces coordinate (x,y,z)</param>
        /// <param name="colors">Array of verteces color (r,g,b,a)</param>
        public static void DrawLine(float[] verteces, float[] colors)
        {
            
        }
        
        #endregion


        #region Render tools

        /// <summary>
        ///     Render text on screen
        /// </summary>
        /// <param name="text">Text for reder</param>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        /// <param name="scale">Font scale</param>
        public static void RenderText(string text, float x, float y, float scale)
        {
            
        }

        public static void RenderSprite(/*Sprite sprite*/)
        {
               
        }
        
        
        
        #endregion
    }
}