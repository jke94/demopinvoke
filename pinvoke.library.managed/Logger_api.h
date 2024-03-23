#ifndef LOGGER_API_H
#define LOGGER_API_H

#include <string>
#include "Logger.h"
#include "ILogger.h"

#if defined(_WIN32) || defined(__CYGWIN__)
	#define __FILENAME__ (strrchr(__FILE__, '\\') ? strrchr(__FILE__, '\\') + 1 : __FILE__)
#elif defined(__linux__)
	#define __FILENAME__ (strrchr(__FILE__, '/') ? strrchr(__FILE__, '/') + 1 : __FILE__)
#else
	#error Ey! Unknown environment!
#endif

void init_logger(void(*native_logger_callback)(const char* msg));
void end_logger();

void log_info_def(const char* file, const char* function, const int line, const std::string& msg);
#define WRITE_INFO(msg) log_info_def(__FILENAME__, __FUNCTION__, __LINE__ , msg)


#endif