import javax.swing.*;
import java.awt.*;

public class App{

    public static void main(String[] args){
        EventQueue.invokeLater(new Runnable(){
            public void run() {
                Game gameBoard = new Game();
                gameBoard.setVisible(true);

            }
        });
    }
}
