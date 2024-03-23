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

void Logger::log_message(const char* file, const char* function, const int line, const char* msg)
{
	if (native_logger_callback_)
	{
		native_logger_callback_(Logger::log(file, function, line, msg).c_str());
	}
}

std::string Logger::log(const char* file, const char* function, const int line, const char* msg)
{
	mtx.lock();
	
	std::string tmp_file(file);
    std::string tmp_function(function);
	std::string temp_msg = "[" + tmp_file + ":" + tmp_function + ":" + std::to_string(line) + "] " + msg;

	mtx.unlock();

	return temp_msg;
}