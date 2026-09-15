//請自行放入SKCOM.dll檔

#ifdef _WIN64
#import "DLL/x64/SKCOM.dll"
#else
#import "DLL/x86/SKCOM.dll"
#endif

#include<iostream>
#include<string>

using namespace std;