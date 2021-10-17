#include "device.h"

#include <utility>

namespace ocl1
{
	device::device()
	{
	}

	device::device(cl_device_id id, std::string name, const bool is_unified_memory_subsystem):
		id(id),
		name(std::move(name)),
		is_unified_memory_subsystem(is_unified_memory_subsystem)
	{
	}
}
