using OpenTK.Mathematics;

namespace SimpleGameEngine.Commons;

public enum Anchor
{
    Center,
    Left,
    Right,
    Top,
    Bottom,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public static class AnchorOperations
{
    public static Vector2 GetCenter(Anchor anchor, Vector2 coordinates, Vector2 size) {
        Vector2 newCoords = coordinates;

        switch (anchor)
        {
            case Anchor.Left:
                newCoords.X += size.X / 2;
                break;
            case Anchor.Right:
                newCoords.X -= size.X / 2;
                break;
            case Anchor.Top:
                newCoords.Y -= size.Y / 2;
                break;
            case Anchor.Bottom:
                newCoords.Y += size.Y / 2;
                break;
            case Anchor.TopLeft:
                newCoords.X += size.X / 2;
                newCoords.Y -= size.Y / 2;
                break;
            case Anchor.TopRight:
                newCoords.X -= size.X / 2;
                newCoords.Y -= size.Y / 2;
                break;
            case Anchor.BottomLeft:
                newCoords.X += size.X / 2;
                newCoords.Y += size.Y / 2;
                break;
            case Anchor.BottomRight:
                newCoords.X -= size.X / 2;
                newCoords.Y += size.Y / 2;
                break;
        }
        
        return newCoords;
    }
    
    public static Vector2 GetTopLeft(Anchor anchor, Vector2 coordinates, Vector2 size) {
        Vector2 newCoords = coordinates;

        switch (anchor)
        {
            case Anchor.Left:
                newCoords.Y += size.Y / 2;
                break;
            case Anchor.Right:
                newCoords.Y += size.Y / 2;
                newCoords.X -= size.X;
                break;
            case Anchor.Top:
                newCoords.X -= size.X / 2;
                break;
            case Anchor.Bottom:
                newCoords.X -= size.X / 2;
                newCoords.Y += size.Y;
                break;
            case Anchor.Center:
                newCoords.X -= size.X / 2;
                newCoords.Y += size.Y / 2;
                break;
            case Anchor.TopRight:
                newCoords.X -= size.X;
                break;
            case Anchor.BottomLeft:
                newCoords.Y += size.Y;
                break;
            case Anchor.BottomRight:
                newCoords.X -= size.X;
                newCoords.Y += size.Y;
                break;
        }
        
        return newCoords;
    }
    
    public static Vector2 GetFromCenter(Anchor anchor, Vector2 coordinates, Vector2 size) {
        Vector2 newCoords = coordinates;

        switch (anchor)
        {
            case Anchor.Left:
                newCoords.X -= size.X / 2;
                break;
            case Anchor.Right:
                newCoords.X += size.X / 2;
                break;
            case Anchor.Top:
                newCoords.Y += size.Y / 2;
                break;
            case Anchor.Bottom:
                newCoords.Y -= size.Y / 2;
                break;
            case Anchor.TopLeft:
                newCoords.X -= size.X / 2;
                newCoords.Y += size.Y / 2;
                break;
            case Anchor.TopRight:
                newCoords.X += size.X / 2;
                newCoords.Y += size.Y / 2;
                break;
            case Anchor.BottomLeft:
                newCoords.X -= size.X / 2;
                newCoords.Y -= size.Y / 2;
                break;
            case Anchor.BottomRight:
                newCoords.X += size.X / 2;
                newCoords.Y -= size.Y / 2;
                break;
        }
        
        return newCoords;
    }
}


// TODO: code that there was useless, but distributes vertices in order TopLeft, TopRight, BottomRight, BottomLeft
//
// byte[] coordIndices = new byte[4];
// (byte, byte) topValues = (0, 0);
//
// for (byte i = 1; i < 4; i++)
// {
//     if (newCoords[i, 1] >= topValues.Item1)
//     {
//         topValues.Item2 = topValues.Item1;
//         topValues.Item1 = i;
//     }
// }
//
// if (newCoords[topValues.Item1, 0] < newCoords[topValues.Item2, 0])
// {
//     coordIndices[0] = topValues.Item1;
//     coordIndices[1] = topValues.Item2;
// }
// else
// {
//     coordIndices[0] = topValues.Item2;
//     coordIndices[1] = topValues.Item1;
// }
//
// List<byte> possibleIndices = new List<byte>() { 0, 1, 2, 3 };
// possibleIndices.Remove(topValues.Item1);
// possibleIndices.Remove(topValues.Item2);
//
// if (newCoords[possibleIndices[0], 0] > newCoords[possibleIndices[1], 0])
// {
//     coordIndices[3] = possibleIndices[0];
//     coordIndices[4] = possibleIndices[1];
// }
// else
// {
//     coordIndices[3] = possibleIndices[1];
//     coordIndices[4] = possibleIndices[0];
// }