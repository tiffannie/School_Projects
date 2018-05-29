/* openreaddir.c by mind [mind@metalshell.com]
 *
 * Example on using opendir, closedir, and readdir to open a directory
 * stream and read in and print file names.
 *
 * 06/04/03
 *
 * http://www.metalshell.com/
 *
 */

#include <stdio.h>
#include <sys/types.h>
#include <dirent.h>
#include <errno.h>

int main(int argc, char *argv[])
{
        DIR             *dip;
        struct dirent   *dit;

        int             i = 0;

        /* check to see if user entered a directory name */
        if (argc < 2)
        {
                printf("Usage: %s <directory>\n", argv[0]);
                return 0;
        }

        /* DIR *opendir(const char *name);
         *
         * Open a directory stream to argv[1] and make sure
         * it's a readable and valid (directory) */
        if ((dip = opendir(argv[1])) == NULL)
        {
                //perror("opendir");//prints out error
                //tests different errors.
		if (errno==ENOTDIR)
		{
			printf("Not Directory\n");
		}
		if (errno==EACCES)
		{
			printf("Permission Denied\n");	
		}
		if (errno==ENOENT)
		{
			printf("File not Found\n");
		}
                return 0;
        }

        printf("Directory stream is now open\n");

        /*  struct dirent *readdir(DIR *dir);
         *
         * Read in the files from argv[1] and print */
        while ((dit = readdir(dip)) != NULL)
        {
                i++;
                if (dit->d_type==DT_DIR)
			printf("\nDirectory: %s", dit->d_name, dit->d_type);
		else
			printf("\n%s", dit->d_name, dit->d_type);

        }

        printf("\n\nreaddir() found a total of %i files\n", i);

        /* int closedir(DIR *dir);
         *
         * Close the stream to argv[1]. And check for errors. */
        if (closedir(dip) == -1)
        {
                perror("closedir");
                return 0;
        }

        printf("\nDirectory stream is now closed\n");
        return 1;
}


