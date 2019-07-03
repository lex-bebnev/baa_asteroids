		
	static class Renderer {
		delegate void RenderMethod(GameObject obj);

        private static RenderMethod _currentRenderMethod;
        
		private static RenderMethod _spriteRenderMethod;
        private static RenderMethod _polygonRenderMethod;
        
		
        public static void SetupRenderer(GameWindow window)
        {
			...
            SetRenderMethods();
        }

		private static void SetRenderMethods()
        {
            _spriteRenderMethod = new RenderMethod(RenderSprite);
            _polygonRenderMethod = new RenderMethod(RenderPolygon);
            _currentRenderMethod = _polygonRenderMethod;
        }
		
        public static void Render(GameObject obj)
        {
            _currentRenderMethod(obj);
        }

        public static void ChangeRenderMethod(RenderModes mode)
        {
            switch (mode)
            {
                case 0: 
                    _currentRenderMethod = _spriteRenderMethod;
                    break;
                case 1: 
                    _currentRenderMethod = _polygonRenderMethod;
                    break;
            }
        };
	}