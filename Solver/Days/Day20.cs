using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day20 : DayBase
{
	public override long Part1(string inputStr)
	{
		(var imageEnchancementAlgorithm, var image) = Parse(inputStr);
		var level = 2;
		var enchanceImage = EnchanceImage(imageEnchancementAlgorithm, image, level);
		var zone = image.zone.Growen(level, level, level, level);
		return enchanceImage.pixels.Count(pt => zone.Contains(pt));
	}

	// Too high 21617
	public override long Part2(string inputStr)
	{
		(var imageEnchancementAlgorithm, var image) = Parse(inputStr);
		var level = 50;
		var enchanceImage = EnchanceImage(imageEnchancementAlgorithm, image, level);
		return enchanceImage.pixels.Count;
	}

	private Image EnchanceImage(Dictionary<int, bool> imageEnchancementAlgorithm, Image image, int zoomLevel)
	{
		var newImage = new Image(image.pixels, image.zone.Growen(zoomLevel, zoomLevel, zoomLevel, zoomLevel));

		for (int i = 0; i < zoomLevel; i++)
			newImage = DoEnchanceImage(newImage, imageEnchancementAlgorithm, i);

		return newImage;
	}

	private Image DoEnchanceImage(Image image, Dictionary<int, bool> imageEnchancementAlgorithm, int zoomLevel)
	{
		var IsTrap = imageEnchancementAlgorithm[0] && imageEnchancementAlgorithm[255];
		var newPixels = new HashSet<Point>();
		var newZone = image.zone;
		for (int x = image.zone.Left; x <= image.zone.Right; x++)
		{
			for (int y = image.zone.Bottom; y <= image.zone.Top; y++)
			{
				int IEAIndex = GetIEAIndex(image.pixels, x, y, image.zone, IsTrap, zoomLevel);
				if (imageEnchancementAlgorithm[IEAIndex])
					newPixels.Add(new Point(x, y));
			}
		}
		return new Image(newPixels, newZone);
	}

	private static int GetIEAIndex(HashSet<Point> pixels, int x, int y, RectInt zone, bool isTrap, int zoomLevel)
	{
		var IEAIndex = 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x - 1, y + 1)) ? 256 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x, y + 1)) ? 128 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x + 1, y + 1)) ? 64 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x - 1, y)) ? 32 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x, y)) ? 16 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x + 1, y)) ? 8 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x - 1, y - 1)) ? 4 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x, y - 1)) ? 2 : 0;
		IEAIndex += IsPixelLit(zone, isTrap, pixels, zoomLevel, new Point(x + 1, y - 1)) ? 1 : 0;
		return IEAIndex;

	}

	private static bool IsPixelLit(RectInt zone, bool isTrap, HashSet<Point> pixels, int zoomLevel, Point point)
	{
		if (zone.Contains(point))
			return pixels.Contains(point);
		else if (!isTrap)
			return false;
		else
			return zoomLevel % 2 == 1;
	}

	private (Dictionary<int, bool> imageEnchancementAlgorithm, Image image) Parse(string inputStr)
	{
		var pixels = new HashSet<Point>();
		var splitted = inputStr.Split("\n\n");
		var imageEnchancementAlgorithm = splitted[0];
		var zone = new RectInt(0, 0, 0, 0);

		var imageLines = splitted[1].Split("\n");
		for (int y = 0; y < imageLines.Length; y++)
		{
			for (int x = 0; x < imageLines.Length; x++)
			{
				if (imageLines[y][x] == '#')
				{
					var flipedY = imageLines.Length - y - 1;
					zone.GrowToInclude(new Point(x, flipedY));
					pixels.Add(new Point(x, flipedY));
				}
			}
		}
		var image = new Image(pixels, zone);
		var dict = new Dictionary<int, bool>();
		for (int i = 0; i < imageEnchancementAlgorithm.Length; i++)
			dict.Add(i, imageEnchancementAlgorithm[i] == '#');

		return (dict, image);
	}

	public record Image(HashSet<Point> pixels, RectInt zone);

}
