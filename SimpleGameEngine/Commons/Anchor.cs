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
    public static float[] GetCenter(Anchor anchor, float[] coordinates, float[] size) {
        float[] newCoords = new float[2];
        Array.Copy(coordinates, newCoords, 2);

        switch (anchor)
        {
            case Anchor.Left:
                newCoords[0] += size[0] / 2;
                break;
            case Anchor.Right:
                newCoords[0] -= size[0] / 2;
                break;
            case Anchor.Top:
                newCoords[1] -= size[1] / 2;
                break;
            case Anchor.Bottom:
                newCoords[1] += size[1] / 2;
                break;
            case Anchor.TopLeft:
                newCoords[0] += size[0] / 2;
                newCoords[1] -= size[1] / 2;
                break;
            case Anchor.TopRight:
                newCoords[0] -= size[0] / 2;
                newCoords[1] -= size[1] / 2;
                break;
            case Anchor.BottomLeft:
                newCoords[0] += size[0] / 2;
                newCoords[1] += size[1] / 2;
                break;
            case Anchor.BottomRight:
                newCoords[0] -= size[0] / 2;
                newCoords[1] += size[1] / 2;
                break;
        }
        
        return newCoords;
    }
    
    public static float[] GetTopLeft(Anchor anchor, float[] coordinates, float[] size) {
        float[] newCoords = new float[2];
        Array.Copy(coordinates, newCoords, 2);

        switch (anchor)
        {
            case Anchor.Left:
                newCoords[1] += size[1] / 2;
                break;
            case Anchor.Right:
                newCoords[1] += size[1] / 2;
                newCoords[0] -= size[0];
                break;
            case Anchor.Top:
                newCoords[0] -= size[0] / 2;
                break;
            case Anchor.Bottom:
                newCoords[0] -= size[0] / 2;
                newCoords[1] += size[1];
                break;
            case Anchor.Center:
                newCoords[0] += size[0] / 2;
                newCoords[1] -= size[1] / 2;
                break;
            case Anchor.TopRight:
                newCoords[0] -= size[0];
                break;
            case Anchor.BottomLeft:
                newCoords[1] += size[1];
                break;
            case Anchor.BottomRight:
                newCoords[0] -= size[0];
                newCoords[1] += size[1];
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