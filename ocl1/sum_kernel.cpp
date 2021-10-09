#include "sum_kernel.h"

sum_kernel::sum_kernel(const execution_context& execution_context_instance, cl_kernel kernel)
	: kernel_contract<sum_kernel_argument, sum_kernel_response>(execution_context_instance, kernel)
{
}
