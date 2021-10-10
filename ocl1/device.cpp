#include "device.h"

#include <utility>

device::device()
{
}

device::device(cl_device_id id, std::string name)
	: id(id),
	  name(std::move(name))
{
}
