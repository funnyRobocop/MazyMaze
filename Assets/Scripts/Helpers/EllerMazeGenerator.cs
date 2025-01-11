using UnityEngine;
using System.Collections;

public class EllerMazeGenerator
{

	public const char MAZE_WALL = '#';
	const char MAZE_PATH = '+';
	const int  UNDETERMINED = -2;
	const int  SET_WALL = -1;
	
	int       rows;           //строки    
	int       cols;           //столбцы
	
	int       act_rows;       //Текущий номер строки
	int       act_cols;       //Текущий номер столбца
	
	int[]     current;        //текущая строка, за исключением наружных стен
	int[]     next;           //следующая строка, за исключением наружных стен
	
	int       numSet;         //Проверка на совпадение
	private int fNext;
	private int fNext2;
	
	public char[,] Maze { get; private set; }         //Поле лабиринта

	
	/* конструктор */
	public EllerMazeGenerator (int nRows, int nCols)
	{
		act_rows = nRows;
		act_cols = nCols;
		
		rows = act_rows*2+1;
		cols = act_cols*2+1;
		
		Maze   = new char[rows,cols];
		current = new int[act_cols*2-1];
		next    = new int[act_cols*2-1];		
		
		/* Sets the maze to filled */
		for(int i =0; i<Maze.GetLength(0); i++){
			for(int j=0; j<Maze.GetLength(1); j++){
				Maze[i,j] = MAZE_WALL;
			}
		}		
		
		for(int i=0; i<next.Length; i++){
			next[i] = UNDETERMINED;
		}	
		
		/* Инициализация первой строки */
		for(int i=0; i<current.Length; i+=2){
			current[i] = i/2+1;
			if(i != current.Length-1)
				current[i+1] = SET_WALL;
		}
		numSet = current[current.Length-1];

		makeMaze ();
	}

	private bool GetRandomBool()
	{
		return (Random.value > 0.5f);
	}	
	
	public void makeMaze()
	{		
		for(int q=0; q<act_rows-1; q++){   //для всех строк кроме последней
			
			if(q != 0){
				
				/* получим текущую строку из последней итерации*/
				for(int i=0; i<current.Length; i++){
					current[i] = next[i];
					next[i] = UNDETERMINED;
				}
			}			
			
			joinSets();
			makeVerticalCuts();			
			
			/* заполним остальную часть следующей строки */			
			for(int j=0; j<current.Length; j+=2){
				
				if(next[j] == UNDETERMINED)
					next[j] = ++numSet;
				
				if(j != current.Length-1)
					next[j+1] = SET_WALL;
			}			
			
			/* запишем текущую строку в поле */
			for(int k=0; k<current.Length; k++){
				
				if(current[k] == SET_WALL){
					Maze[2*q+1,k+1] = MAZE_WALL;
					Maze[2*q+2,k+1] = MAZE_WALL;
				}else{
					Maze[2*q+1,k+1] = MAZE_PATH;
					
					if(current[k] == next[k]){
						Maze[2*q+2,k+1] = MAZE_PATH;
					}
				}				
			}			
		}
		
		makeLastRow();	
	}
	
	private void joinSets()
	{		
		/* Случайные наборы */
		for(int i=1; i<current.Length-1; i+=2){ //проверка только где стены			
			/* Проверка на объединение:
             *      Имеют ли стену между ними,
             *      не являются ли частью одного набора
             *Получаем случайный набор, при объединении
             */
			if(current[i] == SET_WALL && current[i-1] != current[i+1]
			   && GetRandomBool()){				
				current[i] = 0; //Убрать стену
				
				int old  = Mathf.Max(current[i-1],current[i+1]);
				fNext= Mathf.Min(current[i-1],current[i+1]);
				
				// Объединяем два набора в один				
				for(int j=0; j<current.Length; j++){
					
					if(current[j] == old)
						current[j] = fNext;
				}
			}
		}
	}	
	
	/* Случайно выбрать вертикальные пути для каждого набора, убедившись, 
     * что есть по крайней мере 1 вертикальный путь в наборе
     */
	private void makeVerticalCuts()
	{		
		int      begining;     //Начало набора(Включительно)
		int      end;          //Конец набор(Включительно)
		
		bool madeVertical;  //Создание вертикального прохода		
		
		int i;
		begining = 0;
		do{			
			/* Поиск конца */
			i=begining;
			while(i<current.Length-1 && current[i] == current[i+2]){
				i+=2;
			}
			end = i;
			
			//Наличие петли 		
			madeVertical = false;
			do{
				for(int j=begining; j<=end; j+=2){
					
					if(GetRandomBool()){
						next[j] = current[j];
						madeVertical = true;
					}
				}
			}while(!madeVertical);
			
			begining = end+2;  //перейти к следующему набору в строке
			
		}while(end != current.Length-1);
	}
	
	private void makeLastRow()
	{		
		/* Получение текущей строки */
		for(int i=0; i<current.Length; i++){
			current[i] = next[i];
		}
		
		for(int i=1; i<current.Length-1; i+=2){
			
			if(current[i] == SET_WALL && current[i-1] != current[i+1]){
				
				current[i] = 0;
				
				int old  = Mathf.Max(current[i-1],current[i+1]);
				fNext2= Mathf.Min(current[i-1],current[i+1]);
				
				// Объединяем два набора в один
				for(int j=0; j<current.Length; j++){
					
					if(current[j] == old)
						current[j] = fNext2;
				}
			}
		}		
		
		/* Добавление последней строки */
		for(int k=0; k<current.Length; k++){
			
			if(current[k] == SET_WALL){
				Maze[rows-2,k+1] = MAZE_WALL;
			}else{
				Maze[rows-2,k+1] = MAZE_PATH;
			}
		}		
	}
	
	public void printMaze()
	{
		string outp = "";
		for(int i=0; i<cols; i++){
			for(int j=0; j<rows; j++){
				outp += Maze[j,i];
			}
			outp += "\n";
		}
		Debug.Log (outp);
	}
}