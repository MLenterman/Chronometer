#include "pch.h"
#include <iostream>

#define DLLEXPORT __declspec (dllexport)

int main()
{
    std::cout << "Hello World!\n"; 
}

extern "C" {
	DLLEXPORT int TestFunction() 
	{
		return 10;
	}
}

