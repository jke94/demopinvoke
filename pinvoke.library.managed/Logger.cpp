#include <mutex>
#include "Logger.h"

std::mutex mtx;

Logger::Logger(void(*native_logger_callback)(const char* str))
{
	native_logger_callback_ = native_logger_callback;
}

Logger::~Logger()
{

}

void Logger::log_debug_message(const char* file, const char* function, const int line, const char* msg)
{
	if (native_logger_callback_)
	{
		native_logger_callback_(Logger::log(file, function, line, msg, LOG_LEVEL::DEBUG).c_str());
	}
}

void Logger::log_info_message(const char* file, const char* function, const int line, const char* msg)
{
	if (native_logger_callback_)
	{
		native_logger_callback_(Logger::log(file, function, line, msg, LOG_LEVEL::INFO).c_str());
	}
}

void Logger::log_warning_message(const char* file, const char* function, const int line, const char* msg)
{
	if (native_logger_callback_)
	{
		native_logger_callback_(Logger::log(file, function, line, msg, LOG_LEVEL::WARNING).c_str());
	}
}

void Logger::log_error_message(const char* file, const char* function, const int line, const char* msg)
{
	if (native_logger_callback_)
	{
		native_logger_callback_(Logger::log(file, function, line, msg, LOG_LEVEL::ERROR).c_str());
	}
}

std::string Logger::log_level_to_str(LOG_LEVEL log_level)
{
	std::string value = "LOG_LEVEL_NONE";

	switch (log_level)
	{
		case LOG_LEVEL::DEBUG:
			value = "DEBUG";
			break;
		case LOG_LEVEL::INFO:
			value = "INFO";
			break;
		case LOG_LEVEL::WARNING:
			value = "WARNING";
			break;
		case LOG_LEVEL::ERROR:
			value = "ERROR";
			break;
		default:
			value = "LOG_LEVEL_WTF";
			break;
	}

	return value;
}

std::string Logger::log(const char* file, const char* function, const int line, const char* msg, LOG_LEVEL logger_level)
{
	mtx.lock();
	
	std::string tmp_file(file);
    std::string tmp_function(function);
	std::string temp_msg = "[" + log_level_to_str(logger_level) + "][" +
		tmp_file + ":" + tmp_function + ":" +
		std::to_string(line) + "] " + msg;

	mtx.unlock();

	return temp_msg;
}