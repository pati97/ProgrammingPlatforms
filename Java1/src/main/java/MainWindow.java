import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.Random;

class MainWindow extends JFrame {
    private JPanel mainPanel = new JPanel();
    private JButton button1;
    private static int locationX = 100;
    private static int locationY = 100;
    private int width;
    private int heigth;
    public MainWindow() {
        add(mainPanel);
        setSize(1000, 1000);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setContentPane(mainPanel);

        button1 = new JButton("Click");
        button1.setPreferredSize(new Dimension(200,100));
        button1.setVisible(true);
        mainPanel.add(button1);
        button1.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                width = mainPanel.getWidth();
                heigth = mainPanel.getHeight();
                Random rand = new Random();
                locationX = rand.nextInt(width - button1.getWidth());
                locationY = rand.nextInt(heigth - button1.getHeight());
                button1.setLocation(locationX, locationY);
                }
        });
    }
}





