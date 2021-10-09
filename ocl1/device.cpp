#include "device.h"

device::device()
{
}

device::device(cl_device_id id, const std::string& name): id(id),
                                                          name(name)
{
}
