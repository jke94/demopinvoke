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

void log_debug_def(const char* file, const char* function, const int line, const std::string& msg)
{
	logger->log_debug_message(file, function, line, msg.c_str());
}

void log_info_def(const char* file, const char* function, const int line, const std::string& msg)
{
	logger->log_info_message(file, function, line, msg.c_str());
}

void log_warning_def(const char* file, const char* function, const int line, const std::string& msg)
{
	logger->log_warning_message(file, function, line, msg.c_str());
}

void log_error_def(const char* file, const char* function, const int line, const std::string& msg)
{
	logger->log_error_message(file, function, line, msg.c_str());
}
