#include <iostream>
#include <queue>
#include <string>
#include <cstring>
#include <cstdlib>
#include<fstream>
#include<dirent.h>
#include <unistd.h>
#include<sys/types.h>
#include<sys/stat.h>
#include<fcntl.h>
#include<stdlib.h>
#include<stdio.h>
#include <sys/wait.h>
#include "shello.h"

using namespace std;

void echo( char ** input, int count);
void ls();
void cd(char *);
void mkdir(char *path);
void rmdir(char *path, int garbageNumberSoThatFunctionDoesntGetConfusedWithCPLUSPLUSFunctionLikeWhat);
void execute(char **argv);
void execute_normal(char **tokenLists);
void execute_redirect(char **tokenLists, int num);

int main()
{
	while(true)
	{
		char input_string[100];
		char curr_dir[100];
		getcwd(curr_dir, 100);
		//cout << "> " << endl;
		cout << "user@shell-VirtualBox:~" << curr_dir << "$ ";
//*****************************************************************************
		fgets(input_string, 100, stdin);
		if(strcmp(input_string,"exit\n") == 0) { break;}

		char *token = strtok(input_string, " \n");

		char *tokenLists[100];
		int numTokens = 0;
		while(token != NULL)                    	//while char array is not empty
		{
			tokenLists[numTokens] = token;        	//add to array of pointers

			numTokens ++;
			token = strtok(NULL, " \n");           	//split by space
		}
		tokenLists[numTokens] = NULL;
//*****************************************************************************
		if(strcmp(tokenLists[0],"echo") ==0){
			echo(tokenLists, numTokens);
		}
		else if(strcmp(tokenLists[0],"ls") ==0){
			ls();
			cout << "\n" << endl;
		}
		else if(strcmp(tokenLists[0],"cd") ==0){
			cd(tokenLists[1]);
		}
		else if(strcmp(tokenLists[0],"mkdir") ==0){
			mkdir(tokenLists[1]);
		}
		else if(strcmp(tokenLists[0],"rmdir") ==0){
			rmdir(tokenLists[1], 1);
		}
		else {
			if (numTokens == 3){
				if(strcmp(tokenLists[numTokens - 2], ">" )== 0){
						execute_redirect(tokenLists, numTokens);
				}
				else {
					execute_normal(tokenLists);
				}
			}
			else {
			execute_normal(tokenLists);
			}
			//cout << "Please, enter a VALID command, home boy" << endl;
		}
	}
	return 0;
}


void execute_normal(char **tokenLists)
{
	pid_t pid;
	int status;

	pid = fork();
	if (pid < 0)
	{
		printf("ERROR fork failed\n");
		exit(1);
	}
	else if (pid == 0) //child thread
	{
		if (execvp(*tokenLists, tokenLists) < 0)
		{
			perror("execvp");
			exit(1);
		}
	}
	else
	{
		while (wait(&status) != pid) {} //parent, waits for completion
	}

}
void execute_redirect(char **tokenLists, int num)
{
	pid_t pid;
	int status;
	int defout;
	int fd;
	pid = fork();
	if (pid < 0)
	{
		printf("ERROR fork failed\n");
		exit(1);
	}
	else if (pid == 0) //child thread
	{
		defout = dup(1);
		fd=open(tokenLists[2],O_RDWR|O_CREAT,0777);
		tokenLists[num-1] = NULL;
		tokenLists[num-2] = NULL;
		dup2(fd,1);
		if (execvp(*tokenLists, tokenLists) < 0)
		{
			perror("execvp");
			exit(1);
		}
		close(fd);
		dup2(defout,1);
	}
	else
	{
		while (wait(&status) != pid) {} //parent, waits for completion
	}

}

void cd(char* f)
{
	int numb = chdir(f);	// Navigate to the directory specified by the user
	if(numb == -1) {
		cout << "error: directory does not exist" << endl;
	}
}

void mkdir(char *path)
{
	int dirr = mkdir(path, S_IRUSR | S_IWUSR | S_IXUSR);
	if(dirr == -1) {
		cout << "error" << endl;
	}
}

void rmdir(char *path, int some_number)
{
	int dirr = rmdir(path);
	if(dirr == -1) {
		cout << "error: the directory you are deleting still has files or the directory does not exist" << endl;
	}
}
void echo(char ** pointer_array, int count) //girl, yo function is whack. prih'in funky imgs n stuff
{
	for(int i = 1 ; i < count; i ++ ){
		cout << pointer_array[i] <<" ";
	}
	cout << endl;
}

void ls()
{
	char curr_dir[100]; //initializing the pointer to NULL is garbage. give it space for a cwd
	DIR *dp;
	struct dirent *dptr;
	unsigned int count = 0;
							// Get the value of environment variable PWD
	//curr_dir = getenv("PWD"); aka GARB
	getcwd(curr_dir, 100);
	//cout << curr_dir << endl;
	if(NULL == curr_dir)
	{
		printf("\n ERROR : Could not get the working directory\n");
		return;
	}
							// Open the current directory
	dp = opendir((const char*)curr_dir);
	if(NULL == dp)
	{
		printf("\n ERROR : Could not open the working directory\n");
		return;
	}
	printf("\n");
							// Go through and display all the names (files or folders)
							// Contained in the directory.
	for(count = 0; NULL != (dptr = readdir(dp)); count++)
	{
		printf("%s  ",dptr->d_name);
	}
	printf("%s", "\n");
}
