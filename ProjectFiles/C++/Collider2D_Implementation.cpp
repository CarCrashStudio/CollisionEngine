// This file is used for the implementation of all functions written inside the header file
// Author: Trey Hall
// Date 8/1/2018
#include "Collider2D.h"

Collider2D::Collider2D() {

}

// Accessor functions
int Collider2D::GetHeight() {
	return height;
}
int Collider2D::GetWidth() {
	return width;
}

// Setter Functions
void Collider2D::SetWidth(int val) {
	width = val;
}
void Collider2D::SetHeight(int val) {
	height = val;
}