# Pixel Wall-E Programming Language Summary

## Basic Instructions

1. **`Spawn(int x, int y)`**  
   - Initializes Wall-E's position on the canvas.  
   - **Example:** `Spawn(0, 0)` places it at the top-left corner.  
   - **Error:** Fails if coordinates are outside the canvas.  

2. **`ReSpawn(int x, int y)`** *(New feature)*  
   - Moves Wall-E to new coordinates `(x, y)`.  
   - **Example:** `ReSpawn(50, 50)` centers it (on a 100×100 canvas).  
   - **Error:** Fails if new coordinates are invalid.  

3. **`Color(string color)`**  
   - Sets the brush color. Some supported colors:  
     `"Red"`, `"Blue"`, `"Green"`, `"Yellow"`, `"Orange"`, `"Purple"`, `"Black"`, `"White"`, `"Transparent"`.  
   - **Default:** `"Transparent"` (no effect on canvas).  

4. **`Size(int k)`**  
   - Adjusts brush thickness (in pixels).  
   - Converts even `k` to the nearest lower odd number.  
   - **Default:** `1` (single-pixel brush).  

5. **`DrawLine(int dirX, int dirY, int distance)`**  
   - Draws a line from Wall-E's current position in direction `(dirX, dirY)` for `distance` pixels.  
   - **Directions:**  
     - `(-1, -1)`: Top-left diagonal  
     - `(1, 0)`: Right  
     - *(See docs for full list)*  
   - **Example:** `DrawLine(1, 0, 5)` draws a 5-pixel line to the right.  

6. **`DrawCircle(int dirX, int dirY, int radius)`**  
   - Draws a circle centered at `(current pos + (dirX, dirY) * distance` with given `radius`.  
   - **Note:** Only the circumference is drawn.  

7. **`DrawRectangle(int dirX, int dirY, int distance, int width, int height)`**  
   - Draws a rectangle centered at the offset position (`dirX`, `dirY` × `distance`).  

8. **`Fill()`**  
   - Flood-fills all connected pixels of the current color with the brush color.  

9. **`DrawPixel(int x, int y)`** *(New feature)*  
   - Draws a single pixel at `(x, y)` using the current brush color.  
   - **Example:** `DrawPixel(10, 10)` colors pixel (10, 10).  

---

## Variables & Expressions
- **Assignment:** `var ← Expression`  
  - **Valid names:** Letters, numbers, `_` (no leading numbers/`_`).  
  - **Example:** `n ← (0 - 5)` *(Represents `-5`)*.  

### Arithmetic Operations
- Supported: `+`, `-`, `*`, `/`, `**` (power), `%` (modulo).  
- **Example:** `k ← 3 + (2 * n)`  

### Boolean Expressions
- Comparisons: `==`, `>=`, `<=`, `>`, `<`.  
- Logical: `&&` (AND), `||` (OR) *(OR has higher precedence)*.  
- **Example:** `x > 0 && y < 100`  

---

## Functions
1. **`GetActualX()` / `GetActualY()`**  
   - Returns Wall-E's current X or Y coordinate.  

2. **`GetCanvasSize()`**  
   - Returns the canvas dimension (assumes square).  

3. **`GetColorCount(string color, int x1, int y1, int x2, int y2)`**  
   - Counts pixels of `color` in the rectangle defined by `(x1,y1)` to `(x2,y2)`.  

4. **`IsBrushColor(string color)`**  
   - Returns `1` if the brush color matches `color`, else `0`.  

5. **`IsCanvasColor(string color, int vertical, int horizontal)`**  
   - Checks if the pixel at `(current pos + offset)` matches `color`.  

6. **`IsBrushSize(int size)`**  
   - Returns `1` if the brush size matches `size`.  

---

## Control Flow
### Labels
- **Syntax:** `label_name` (followed by a newline).  
- **Example:**  
  ```plaintext
  start_loop
  DrawLine(1, 0, 1)

## Conditional Jumps

### Syntax
```plaintext
GoTo [label_name] (boolean condition)