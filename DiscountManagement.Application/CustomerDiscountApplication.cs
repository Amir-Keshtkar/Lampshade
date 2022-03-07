﻿using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application {
    public class CustomerDiscountApplication: ICustomerDiscountApplication {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication (ICustomerDiscountRepository customerDiscountRepository) {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define (DefineCustomerDiscount command) {
            var operation = new OperationResult();
            if(_customerDiscountRepository.Exists(x =>
                   x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var discount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate,
                endDate, command.Reason);
            _customerDiscountRepository.Create(discount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit (EditCustomerDiscount command) {
            var operation = new OperationResult();
            var discount = _customerDiscountRepository.GetById(command.Id);
            if(discount == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if(_customerDiscountRepository.Exists(x =>
                   x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            discount.Edit(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCustomerDiscount? GetDetails (long id) {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search (CustomerDiscountSearchModel searchModel) {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}