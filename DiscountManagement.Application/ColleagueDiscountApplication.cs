using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.VisualBasic;

namespace DiscountManagement.Application {
    public class ColleagueDiscountApplication: IColleagueDiscountApplication {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication (IColleagueDiscountRepository colleagueDiscountRepository) {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define (DefineColleagueDiscount command) {
            var operation = new OperationResult();
            if(_colleagueDiscountRepository.Exists(x =>
                   x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(discount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit (EditColleagueDiscount command) {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepository.GetById(command.Id);
            if(discount == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if(_colleagueDiscountRepository.Exists(x =>
                   x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate &&
                   x.Id != command.Id)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            discount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove (long id) {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepository.GetById(id);
            if(discount == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            discount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore (long id) {
            var operation = new OperationResult();
            var discount = _colleagueDiscountRepository.GetById(id);
            if(discount == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            discount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditColleagueDiscount? GetDetails (long id) {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search (ColleagueDiscountSearchModel searchModel) {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
