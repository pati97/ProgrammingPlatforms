import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.awt.image.ImageObserver;

public class Food implements IDrawable {
    private final int randPosition = 40;
    private Image apple;
    private int appleX;
    private int appleY;
    private int location;
    private final int size = Board.getDotSize();

    public void locateApple() {

        location = (int) (Math.random() * randPosition);
        appleX = ((location * size));

        location = (int) (Math.random() * randPosition);
        appleY = ((location * size));
    }

    public int getFoodX() {

        return appleX;
    }

    public int getFoodY() {
        return appleY;
    }
    public void loadApple()
    {
        ImageIcon iia = new ImageIcon("src/image/apple.png");
        apple = iia.getImage();
    }
    @Override
    public void draw(Graphics graphics) {
        graphics.drawImage(apple, appleX,appleY,20, 20, null);
    }
}
