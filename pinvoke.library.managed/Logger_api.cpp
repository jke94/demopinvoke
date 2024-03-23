#include "Logger_api.h"

ILogger* logger = nullptr;

void init_logger(void(*native_logger_callback)(const char* msg))
{
	logger = new Logger(native_logger_callback);
}

void end_logger()
{
	delete logger;
	logger = nullptr;
}

void log_info_def(const char* file, const char* function, const int line, const std::string& msg)
{
	logger->log_message(file, function, line, msg.c_str());
}