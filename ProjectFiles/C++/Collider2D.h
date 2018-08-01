// Collider2D is one of the main components of the Physics engine
// It handles all collision detection between objects with this collider
// Author: Trey Hall
// Date: 8/1/2018

class Collider2D {
	private:
		int width;
		int height;

	public:
		// Accessor Functions
		int GetWidth();
		int GetHeight();

		// Setter functions
		void SetWidth(int);
		void SetHeight(int);

		// Constructors
		Collider2D();
};