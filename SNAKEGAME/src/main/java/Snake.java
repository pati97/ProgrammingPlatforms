import java.awt.*;
import java.awt.event.KeyEvent;
import javax.swing.ImageIcon;
import java.util.*;

public class Snake implements IDrawable {

    private Direction dir;
    private int dots = 0;
    private Image ball;
    private Image head;
    private final int x[] = new int[Board.getAllDots()];
    private final int y[] = new int[Board.getAllDots()];

    private final int size = Board.getDotSize();

    public int getSnakeX(int index) {
        return x[index];
    }

    public int getSnakeY(int index) {
        return y[index];
    }

    public void setSnakeX(int i) {
        x[0] = i;
    }

    public void setSnakeY(int i) {
        y[0] = i;
    }

    public int getDots()
    {
        return dots;
    }
    public void setDots(int j)
    {
        dots = j;
    }

    public void move() {

        for (int z = dots ; z > 0; z--) {
            x[z] = x[(z - 1)];
            y[z] = y[(z - 1)];
        }

        if (dir == Direction.LEFT) {
            x[0] -= size;
        }

        if (dir == Direction.RIGHT) {
            x[0] += size;
        }

        if (dir == Direction.UP) {
            y[0] -= size;
        }

        if (dir == Direction.DOWN) {
            y[0] += size;
        }
    }

    public void setDirection(Direction direction) {
        dir = direction;
    }
    public void loadHead()
    {
        ImageIcon iih = new ImageIcon("src/image/head.png");
        head = iih.getImage();
    }
    public void loadBall()
    {
        ImageIcon iid = new ImageIcon("src/image/dot.png");
        ball = iid.getImage();
    }

    @Override
    public void draw(Graphics graphics) {

        for (int z = 0; z < getDots(); z++) {
            if (z == 0) {
                graphics.drawImage(head, getSnakeX(z), getSnakeY(z), 20, 20,null) ;
            } else {
                graphics.drawImage(ball, getSnakeX(z), getSnakeY(z),20,20, null);
            }
        }
    }
}
