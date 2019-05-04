import java.awt.*;

public class App{

    public static void main(String[] args){
        EventQueue.invokeLater(new Runnable(){
            public void run() {
                MainWindow frame = new MainWindow();
                frame.setVisible(true);
            }
        });
    }
}
