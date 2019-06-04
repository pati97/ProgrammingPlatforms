import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.Random;
import java.awt.Color;

public class Board extends JPanel implements ActionListener, IDrawable {


    private final static int  boardWidth = 800;
    private final static int boardHeight = 800;

    private final static int pixelSize = 20;
    private final static int allPixel = (boardWidth * boardHeight)/(pixelSize * pixelSize);

    private final static int delay = 150;

    private Snake snake = new Snake();
    private Food food = new Food();

    private boolean inGame = true;
    private Timer timer;

    public Board() {

        initBoard();
    }

    private void initBoard() {

        addKeyListener(new TAdapter());
        setBackground(Color.black);
        setFocusable(true);

        setPreferredSize(new Dimension(boardWidth, boardHeight));

        snake.loadBall();
        snake.loadHead();
        food.loadApple();
        initGame();
    }

    private void initGame() {

        snake.setDots(3);

        for (int z = 0; z < snake.getDots(); z++) {
            snake.setSnakeX(boardWidth / 2);
            snake.setSnakeY(boardHeight / 2);
        }
        snake.setDirection(Direction.RIGHT);
        food.locateApple();

        timer = new Timer(delay, this);
        timer.start();
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);

        draw(g);
    }

    @Override
    public void draw(Graphics graphics) {
        if(inGame)
        {
            food.draw(graphics);
            snake.draw(graphics);
            Toolkit.getDefaultToolkit().sync();
        }
        else
        {
            gameOver(graphics);
        }
    }

    private void gameOver(Graphics g) {

        String msg = "Game Over";
        Font small = new Font("Helvetica", Font.BOLD, 40);
        FontMetrics metrics = getFontMetrics(small);

        g.setColor(Color.orange);
        g.setFont(small);
        g.drawString(msg, (boardWidth - metrics.stringWidth(msg)) / 2, boardHeight / 2);
    }

    private void checkAppleCollision() {

        if ((snake.getSnakeX(0) == food.getFoodX()) && (snake.getSnakeY(0) == food.getFoodY())) {
            //Add a dots to snake
            snake.setDots(snake.getDots() + 1);
            //Create a new food
            food.locateApple();
        }
    }

    private void checkCollision() {
        //If snake makes a crash with itself
        for (int z = snake.getDots(); z > 0; z--) {

            if ((z > 1) && (snake.getSnakeX(0) == snake.getSnakeX(z)) && (snake.getSnakeY(0) == snake.getSnakeY(z))) {
                inGame = false;
            }
        }
        // If the snake intersects with the board edges
        if (snake.getSnakeY(0) >= boardHeight) {
            inGame = false;
        }
        if (snake.getSnakeY(0) < 0) {
            inGame = false;
        }
        if (snake.getSnakeX(0) >= boardWidth) {
            inGame = false;
        }
        if (snake.getSnakeX(0)< 0) {
            inGame = false;
        }
        if (!inGame) {
            timer.stop();
        }
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        if (inGame == true) {

            checkAppleCollision();
            checkCollision();
            snake.move();
        }
        repaint();
    }

    private class TAdapter extends KeyAdapter {

        @Override
        public void keyPressed(KeyEvent e) {

            int key = e.getKeyCode();

            if (key == KeyEvent.VK_LEFT) {
                snake.setDirection(Direction.LEFT);
            }

            if (key == KeyEvent.VK_RIGHT) {
                snake.setDirection(Direction.RIGHT);
            }

            if (key == KeyEvent.VK_UP) {
                snake.setDirection(Direction.UP);
            }

            if (key == KeyEvent.VK_DOWN) {
                snake.setDirection(Direction.DOWN);
            }
        }
    }
    public static int getAllDots() {
        return allPixel;
    }
    public static int getDotSize() {
        return pixelSize;
    }
}


