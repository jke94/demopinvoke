#include "Logger.h"

Logger::Logger(void(*native_logger_callback)(const char* str))
{
	native_logger_callback_ = native_logger_callback;
}

Logger::~Logger()
{

}

void Logger::log_message(const char* str)
{
	if (native_logger_callback_)
	{
		native_logger_callback_(str);
	}
}
