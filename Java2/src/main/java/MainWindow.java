import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.Random;
import java.awt.Color;

class MainWindow extends JFrame {
    private JPanel mainPanel = new JPanel();
    private JButton button1;
    private int startX = 100;
    private int startY = 200;
    private long startTime;
    private double length;
    private double ang;


    public MainWindow() {
        add(mainPanel);
        //setUndecorated(true);
        setExtendedState(JFrame.MAXIMIZED_BOTH);
        //setBackground(new Color(0,0,0,0));
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setContentPane(mainPanel);
        //mainPanel.setBackground(new Color(0,0,0,0));

        button1 = new JButton("");
        button1.setLocation(startX, startY);
        button1.setPreferredSize(new Dimension(100,100));
        button1.setBackground(Color.orange);
        mainPanel.add(button1);


        mainPanel.addMouseListener(new MouseAdapter() {
            @Override
            public void mousePressed(MouseEvent e)
            {
                final Point point = e.getPoint();
                final int currentX = point.x;
                final int currentY = point.y;
                startX = button1.getX();
                startY = button1.getY();
                startTime = System.nanoTime();

                int moveInX = startX - currentX;
                int moveInY = startY - currentY;

                length = Math.sqrt(Math.pow(moveInX,2) + Math.pow(moveInY,2));
                ang = Math.toDegrees(Math.atan2(moveInY, moveInX));
            }
        });

        Timer timer = new Timer(1000 / 20, new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                double duration = (System.nanoTime() - startTime) / 1e6;
                int speed = 150;
                double distanceSoFar = Math.min(speed * duration / 1000d, length);

                int x = startX - (int) (distanceSoFar * Math.cos(Math.toRadians(ang)));
                int y = startY - (int) (distanceSoFar * Math.sin(Math.toRadians(ang)));

                if( x + button1.getWidth() > mainPanel.getWidth())
                {
                    x = mainPanel.getWidth() - button1.getWidth();
                }
                if(x < 0)
                {
                    x = button1.getX();
                }
                if(y<0)
                {
                    y = button1.getY();
                }
                if(y + button1.getHeight()> mainPanel.getHeight())
                {
                    y = mainPanel.getHeight()- button1.getHeight();
                }

                button1.setLocation(x,y);
            }
        });
        timer.setRepeats(true);
        timer.start();

    }
}





