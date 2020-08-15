

class IDCounter{
    private int start;
    private int currentID;
    private int counterStep = 1;
    
    public IDCounter(int start = 0, int counterStep = 1){
        this.start = start;
        this.currentID = start - 1;
        this.counterStep = counterStep;
    }

    public int getNextID(){
        currentID = currentID + counterStep;
        return currentID;
    }
    public string getNextStringID(){
        return getNextID().ToString();
    }

    public int getCurrentID(){
        return currentID;
    }

    public void reset(){
        this.currentID = this.start - 1;
    }
}